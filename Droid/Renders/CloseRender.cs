﻿using System;
using Android.Content;
using Registro.Droid;
using static Registro.Controls.AndroidClosing;

[assembly: Xamarin.Forms.Dependency(typeof(CloseRender))]
namespace Registro.Droid
{
    public class CloseRender : IClose
    {
        public Boolean CloseApp()
        {
            Context c = MainActivity.Instance;
            Intent startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
             c.StartActivity(startMain);
            return true;
        }        
       
    }
}
