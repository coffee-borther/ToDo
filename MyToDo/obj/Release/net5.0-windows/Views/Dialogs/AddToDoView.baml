   M S B A M L   `   `   `  ???? A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MyToDo.Views.DialogsMyToDo.Views.Dialogs  &     MyToDo.Views.Dialogs.AddToDoView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  .local"clr-namespace:MyToDo.Views.Dialogs  $	??280??$	??400??.???   ? RowDefinitions  ?? $
??Auto ??? ?? $
??Auto ?.+??? $??添加待办事项??$??18(?$
??Bold"? ?? Padding$ 20,10q?]? $??False??$??1??.+???  ?? Orientation$ 
Horizontal=?$	??Top=?$??20,10q?.+??? $??状态??$	 5,0q??? $
??10,0q? ?? SelectedIndex ?? *Model.Status+.@??? .??待办?? .??	已完成?? $??Center=?$
??20,0q?)  #MaterialDesignThemes.Wpf.HintAssist   Hint$! 请输入待办事项概要??$	??Top=????? *Model.Title+?? $??20,10q?$! 请输入待办事项内容??$	??Top=? ?? AcceptsReturn$
 True?? ?? TextWrapping$
 Wrap=?$	??110?????? *Model.Content+.???? $ 
Horizontal=?$??2??$??Right=?.+??? $??10, 5q????? *SaveCommand+.??确定?? $
??5, 5q?    MaterialDesignOutlinedButton#??[  ???? *CancelCommand+.??取消