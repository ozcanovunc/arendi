﻿

#pragma checksum "C:\Users\ozcanovunc\Documents\GitHub\arendi\Arendi\Arendi.Windows\HubPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AC8DF208A30F6F0E1DCC179F3EBB94AB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arendi
{
    partial class HubPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 106 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.DeleteIdea_Button_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 107 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.DeleteWorker_Button_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 108 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Refresh_Button_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 109 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Logout_Button_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 96 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.UserSelectionChanged_Event;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 79 "..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.IdeaSelectionChanged_Event;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


