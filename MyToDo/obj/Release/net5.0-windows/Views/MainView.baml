   M S B A M L   `   `   `  ???? A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyToDoMyToDo  3clr-namespace:MyToDo.ExtesionsMyToDo.Extesions      MyToDo.Views.MainView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:MyToDo     .extclr-namespace:MyToDo.Extesions     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   b	 ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	   ? Title$  
MainWindow??$	??600??$
??1000?? ? WindowStartupLocation$ CenterScreen=? ? WindowStyle$
 None=?   MaterialDesignBody#???   $??Regular"?$??13(?&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?   TextRenderingMode$
 Auto=?  MaterialDesignPaper#???    MaterialDesignFont#???   ? AllowsTransparency$
 True??.??) 	 #MaterialDesignThemes.Wpf.DialogHost  -     Name$ 
DialogHost??   
Identifier$
 Root??   DialogTheme$ Inherit=?.??) 	 #MaterialDesignThemes.Wpf.DrawerHost  -   	  Name$	 
drawerHost??
   IsLeftDrawerOpen
 ??  ?? ElementName$ MenuToggleButton?? ?? Path$ 	IsChecked?   LeftDrawerContent ]? $	??220??.+??? $	??Top=?$??20q?.+??? $??50??$??50??$??../Images/user.jfif?? J? Clip @?  @? Center$ 25,25&? @? RadiusX$ 25Q? @? RadiusY$ 25Q??? $??0, 10q?$??Center=????? *
UserName+ ?? Mode$ TwoWay=??? -    ??Name$ MenuBar??  MyListBoxItemStyle#??[ ???? *
MenuBars+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged??*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command ?? *NavigateCommand+   CommandParameter ?? *SelectedItem+ ?? RelativeSource ??  ?? Mode$ FindAncestor=? ?? AncestorType ListBox???? .R???  ?? Orientation$ 
Horizontal=?.+?' 	 !MaterialDesignThemes.Wpf.PackIcon  $??15, 0q?   Kind ?? *Icon+?? ???? *Title+.???  ? RowDefinitions ?? $
??auto ??? .+?'	 	 !MaterialDesignThemes.Wpf.Snackbar	  -    	 Name$ Snackbar??  K? ZIndex$  1??! 	  MessageQueue! 4
 	 .MaterialDesignThemes.Wpf.MessageQueueExtension
  ( 	 "MaterialDesignThemes.Wpf.ColorZone  -   "  Name$" 	ColorZone??$??8q?+ 	 %MaterialDesignThemes.Wpf.ShadowAssist#   ShadowDepth$# Depth2=?$   Mode$$ 
PrimaryMid=?$	??Top=?.??]? $
??True??.+??? $ 
Horizontal=?$??Right=?.+??? -   % ??Name$% btnMin??$	??─?? ! MaterialDesignFlatMidBgButton#??[ ?? -   $% btnMax??$	??☐??#??[ ?? -   $% btnClose??$	??✕??#??[ ' 	 !MaterialDesignThemes.Wpf.PopupBox  $??Right=?&   ToggleContent& ?? $??25??$??25??$??../Images/user.jfif?? @? $ 	12.5,12.5&?$
 12.5Q?$
 12.5Q?'   PopupContent.' ?? .+??? $??注销当前账户?????? *LogoutCommand+?? $ 
Horizontal=?.+?d? -	   ( d?Name$( MenuToggleButton?? ' #MaterialDesignHamburgerToggleButton#??[ ) d? 	IsChecked$) FalseY?4  .System.Windows.Automation.AutomationProperties*   Name$* HamburgerToggleButton???? $??24,0,0,0q?+ ? ToolTip$+ Previous Item??  MaterialDesignToolButton#??[ ???? *GoBackCommand+??0 	 *MaterialDesignThemes.Wpf.PackIconExtension  ,   Kind$, 	ArrowLeft=?-   Size- 24????  ?? " ?$ (TextElement.Foreground)?+ 	 %MaterialDesignThemes.Wpf.RippleAssist.   Feedback. ??  ?? *Self+$ 
Foreground?  BrushRoundConverter/ ?? 	Converter#/ [ ?? $??16,0,0,0q?$+ 	Next Item??#??[ ???? *GoForwardCommand+??  $, 
ArrowRight=?- 24????  ?? " ?$ (TextElement.Foreground)?. ??  ?? *Self+$ 
Foreground?#/ [ ?? $??Right=?$??Center=?$??22(?$??16q?$%* Material Design In XAML Toolkit??$??	记事本???? $??1??"   MyToDo.Extesions.PrismManger0   MainViewRegionName!  Prism.Regions.RegionManager1   
RegionName#1 Z0 