﻿

#pragma checksum "C:\Users\ozcanovunc\Documents\GitHub\arendi\Arendi\Arendi.WindowsPhone\Pages\IdeaPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "18FF13D3C766DE876AD007D45E42C162"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arendi.Pages
{
    partial class IdeaPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.CommandBar appBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.MenuFlyout IdeaPage_FlyOut_Menu; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Data.CollectionViewSource CommentCollection; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox IdeaPage_Comment_TextBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ProgressRing IdeaPage_ProcessRing; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock IdeaPage_IdeaContent_TextBlock; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock IdeaPage_IdeaHeader_TextBlock; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Pages/IdeaPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            appBar = (global::Windows.UI.Xaml.Controls.CommandBar)this.FindName("appBar");
            IdeaPage_FlyOut_Menu = (global::Windows.UI.Xaml.Controls.MenuFlyout)this.FindName("IdeaPage_FlyOut_Menu");
            CommentCollection = (global::Windows.UI.Xaml.Data.CollectionViewSource)this.FindName("CommentCollection");
            IdeaPage_Comment_TextBox = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("IdeaPage_Comment_TextBox");
            IdeaPage_ProcessRing = (global::Windows.UI.Xaml.Controls.ProgressRing)this.FindName("IdeaPage_ProcessRing");
            IdeaPage_IdeaContent_TextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("IdeaPage_IdeaContent_TextBlock");
            IdeaPage_IdeaHeader_TextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("IdeaPage_IdeaHeader_TextBlock");
        }
    }
}


