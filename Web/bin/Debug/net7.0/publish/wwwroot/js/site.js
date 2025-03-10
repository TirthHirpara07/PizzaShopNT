// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SidebarOnClick(){
    
    const sideBar_collapse = document.getElementById("Sidebar");
    sideBar_collapse.classList.toggle("SidebarToggle");       
   
}
function eyeToggle(elementId,img){
    var eye = document.getElementById(elementId);
    if(eye.type == 'password'){
        img.src = "/images/icons/eye open.svg";
        eye.type = "text";
    }
    else{
        img.src = "/images/icons/password not visible.svg";
        eye.type = "password";
    }
}
