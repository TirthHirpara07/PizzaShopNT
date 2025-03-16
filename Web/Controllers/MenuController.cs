using Entity.Helper;
using Entity.Models;
using Entity.ViewModal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Service.Implementations;
using Service.Interfaces;

namespace Web.Controllers;

public class MenuController : Controller
{

    private readonly ICompositeViewEngine _viewEngine;
    private readonly IMenuService _service;
    public MenuController(IMenuService service, ICompositeViewEngine viewEngine)
    {
        _service = service;
        _viewEngine = viewEngine;
    }

    [AuthorizePermission("Menu", "CanView")]
    public async Task<IActionResult> Index(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        msm.CategoryList = await _service.GetCategories();
        p.Category = msm.CategoryList.FirstOrDefault().Id;
        (msm.ItemList, p.count) = _service.GetItemsByCategory(p);
        ViewBag.pageContent = await _service.UpdatePageDetails(p);
        return View(msm);
    }


    public async Task<IActionResult> PartialItemTable(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        (msm.ItemList, p.count) = _service.GetItemsByCategory(p);
        ViewBag.pageContent = await _service.UpdatePageDetails(p);
        return PartialView(msm);
    }

    [AuthorizePermission("Menu", "CanEdit")]
    public async Task<IActionResult> AddCategory(MenuServiceModel model)
    {
        var result = await _service.AddCategory(model.Category);
        return RedirectToAction(result.action, result.controller);
    }

    [AuthorizePermission("Menu", "CanEdit")]
    public async Task<IActionResult> UpdateCategory(MenuServiceModel model)
    {
        var result = await _service.UpdateCategory(model.Category);
        return RedirectToAction(result.action, result.controller);
    }

    [AuthorizePermission("Menu", "CanDelete")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _service.DeleteCategory(id);
        return RedirectToAction(result.action, result.controller);
    }


    public async Task<IActionResult> ModifierPartialView(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        msm.ModifierGroupList = await _service.GetModifierGroups();
        p.Category = msm.ModifierGroupList.FirstOrDefault().Id;
        (msm.ModifierList, p.count) = _service.GetModifierByModifierGroups(p);
        ViewBag.pageContent = await _service.UpdatePageDetails(p);
        return PartialView(msm);
    }

    public async Task<IActionResult> PartialModifierTable(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        (msm.ModifierList, p.count) = _service.GetModifierByModifierGroups(p);
        ViewBag.pageContent = await _service.UpdatePageDetails(p);
        return PartialView(msm);
    }

    //Show Partial View for Updating ModifierGrop
    [HttpPost]
    public IActionResult EditModifierGroups(ShowModifierGroup mg)
    {
        MenuServiceModel msm = new MenuServiceModel();
        var (list, count) = _service.GetModifierByModifierGroups(new ItemPagination { Category = mg.Id, pageSize = 0 });
        msm.ModifierList = list;
        msm.ModifierGroup = mg;
        var html = RenderPartialViewToString("EditModifierGroups", msm);
        return Json(new { html, list = msm.ModifierList });
    }

    public async Task<IActionResult> UpdateModifiersGroup(ShowModifierGroup bean)
    {
        return Ok(await _service.UpdateModifiersGroup(bean));

    }

    public async Task<IActionResult> PartialExistingModifierTable(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        (msm.ModifierList, p.count) = _service.GetModifierByModifierGroups(p);
        ViewBag.pageContentExisting = await _service.UpdatePageDetails(p);
        return PartialView(msm);
    }

    [AuthorizePermission("Menu", "CanDelete")]
    public async Task<IActionResult> DeleteModifierGroup(int id)
    {
        var result = await _service.DeleteModifierGroup(id);
        return RedirectToAction(result.action, result.controller);
    }

    [HttpGet]
    [AuthorizePermission("Menu","CanEdit")]
    public async Task<IActionResult> AddnewModifier()
    {
        List<ShowModifierGroup> modifierGroups = await _service.GetModifierGroups();
        var selectList = modifierGroups.Select(m => new SelectListItem
        {
            Value = m.Id.ToString(),
            Text = m.Name
        }).ToList();
        ViewData["ModifierGroups"]=selectList;
        return PartialView("AddUpdateModifier",new ShowModifier());
    }

    [HttpPost]
    [AuthorizePermission("Menu","CanEdit")]
    public async Task<IActionResult> AddnewModifier(ShowModifier bean)
    {
        var result = await _service.AddnewModifier(bean);
        return RedirectToAction(result.action, result.controller);
    }

    [HttpGet]
    [AuthorizePermission("Menu","CanEdit")]
    public async Task<IActionResult> UpdateModifier(int id)
    {
        
        var selectList = (await _service.GetModifierGroups()).Select(m => new SelectListItem
        {
            Value = m.Id.ToString(),
            Text = m.Name
        }).ToList();
        ShowModifier data = await _service.GetModifier(id);
        ViewData["ModifierGroups"]=selectList;
        return PartialView("AddUpdateModifier",data);
    }
    [AuthorizePermission("Menu","CanEdit")]
    public async Task<IActionResult> UpdateModifier(ShowModifier bean)
    {
        var result = await _service.UpdateModifier(bean);
        return RedirectToAction(result.action, result.controller);
    }
    public async Task<IActionResult> DeleteModifier(List<ShowModifier> list)
    {
        var result = await _service.DeleteModifier(list);
        return RedirectToAction(result.action, result.controller);
    }
















    //convert html in  resopnse into  string html
    private string RenderPartialViewToString(string viewName, object model)
    {
        ViewData.Model = model;
        using (var sw = new StringWriter())
        {
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
            var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw, new HtmlHelperOptions());
            viewResult.View.RenderAsync(viewContext);
            return sw.GetStringBuilder().ToString();
        }
    }




}