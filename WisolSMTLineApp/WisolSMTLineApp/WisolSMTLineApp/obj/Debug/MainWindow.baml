   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5   	        WisolSMTLineApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WisolSMTLineApp     5   	     � Title$  
MainWindow��5   	   $	��480��6   $	��800��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value��%R   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 16(�6)   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��MainTab��5      $��1��6$    5       �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName��%J  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5      " ��6.   .R�5   "   �� $��Border��6)   $��1,1,1,0q�67   
�������$ 2,2,0,0��6i   $	��1,0q�6�   ._�5   &   �� $ ContentSite��67   $��Center=�5   )   $��Center=�5   )   $��Header��5   )   $
��10,5q�5   )   5    #   	 5!   "   P� 5"   &   
 �@��
IsSelected . . �5#   *   �� $ Border��61    � ��
 ��·�6[   5$   '   P� 5%   &   
 �@��
IsSelected .  . �5&   *   �� $ Border��61    � ��
 �����6[   5'   '   5)      5+      5,      5-      .@�5.      �� $
��PLAN��6   $��16(�6$   .��5/      !   WisolSMTLineApp.PlanControl  50      �� 51      $��ORDER��6   $��16(�6%   .��52      "   WisolSMTLineApp.OrderControl  53      �� 54      -   5        $��
MonitorTab��54   5   $��MONITOR��6   $��16(�6'   .��55      (� .��56      $   WisolSMTLineApp.MonitorControl  $��Stretch=�6+   57      58      �� 5:      $��CONFIRMATION��6   $��16(�6,   .��5;      (   "WisolSMTLineApp.ConfirmationWindow  5<      �� 5=      $��SETTING��6   $��16(�6'   .��5>      $   WisolSMTLineApp.SettingControl  5?      5@      5A      5B      