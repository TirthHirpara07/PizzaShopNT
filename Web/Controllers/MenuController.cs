using Entity.Helper;
using Entity.ViewModal;
using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;

namespace Web.Controllers;

public class MenuController : Controller
{
    private readonly IMenuService _service;
    public MenuController(IMenuService service)
    {
        _service = service;
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

     public async Task<IActionResult> PartialModiferTable(ItemPagination p)
    {
        MenuServiceModel msm = new MenuServiceModel();
        (msm.ModifierList, p.count) = _service.GetModifierByModifierGroups(p);
        ViewBag.pageContent = await _service.UpdatePageDetails(p);
        return PartialView(msm);
    }


}