   M S B A M L   `   `   `  ???? A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MyToDo.ExtesionsMyToDo.Extesions  +clr-namespace:MyToDo.ViewsMyToDo.Views      MyToDo.Views.LoginView   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors 	 )passclr-namespace:MyToDo.Extesions 	 &localclr-namespace:MyToDo.Views 	 $	??600??$	??350??#  Prism.Services.Dialogs.Dialog    WindowStyle  ??  ?? 
TargetType" ?.???  ?? Property ? ?? ?? Value$	 600????  ? ??$	 350????  ?@?SizeToContent$ WidthAndHeight=???  ?@?
ResizeMode$ NoResize=???  ?@ WindowStartupLocation$ CenterScreen=?.??? 
??????? ? ColumnDefinitions ?? $
??1.5* ??? .+?'  !MaterialDesignThemes.Wpf.Snackbar  -     Name$ LoginSnakeBar??$??2?? K? ZIndex$ 1??   MessageQueue 4  .MaterialDesignThemes.Wpf.MessageQueueExtension  ?? $??50q?$??/Images/login.png??7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  $??1?? ?? SelectedIndex ?? *SelectedIndex+	 ?? FallbackValue$	 0??.@?<  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .??]? $??15q?$??Center=?.+??? $
??0,10q?$	??Top=?$??22(?$
??Bold"?$??Center=?$??欢迎使用???? $
??0,10q?)  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 请输入账号??$	??Top=????? *	Account+ ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?G? $
??0,10q?$
 请输入密码??$	??Top=?)   #MyToDo.Extesions.PassWordExtensions   PassWord ?? *
Password+$ TwoWay=?$ PropertyChanged=?* 	 $Microsoft.Xaml.Behaviors.Interaction   	Behaviors '	   !MyToDo.Extesions.PasswordBehavior	  ?? $??Login??$??登录系统??$	??Top=????? *ExecuteCommand+]? $	??0,5q?$??False??.+??? $??注册账号??   Triggers +
 	 %Microsoft.Xaml.Behaviors.EventTrigger
   
  	EventName$ MouseLeftButtonDown??* 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ RegisterPage??   Command ?? *ExecuteCommand+?? $??Right=?$??忘记密码???  .??]? $??15q?$??Center=?.+??? $
??0,10q?$	??Top=?$??22(?$
??Bold"?$??注册账号???? $	??0,5q?$
 请输入用户名??$	??Top=????? *UserDto.UserName+?? $	??0,5q?$
 请输入账号??$	??Top=????? *UserDto.Account+G? $	??0,5q?$
 请输入密码??$	??Top=? ?? *UserDto.Password+$ TwoWay=?$ PropertyChanged=? 	  G? $	??0,5q?$
 请再次输入密码??$	??Top=? ?? *UserDto.NewPassWord+$ TwoWay=?$ PropertyChanged=? 	  ?? $??Register??$??注册账号??$	??Top=????? *ExecuteCommand+?? $
??0,10q?$??Return??$??返回登录??$	??Top=?    MaterialDesignOutlinedButton#??[  ???? *ExecuteCommand+