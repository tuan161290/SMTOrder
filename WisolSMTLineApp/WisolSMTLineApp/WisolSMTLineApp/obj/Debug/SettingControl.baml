   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5      =#clr-namespace:WisolSMTLineApp.ModelWisolSMTLineApp.Model  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      $    WisolSMTLineApp.SettingControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WisolSMTLineApp     5      5model#clr-namespace:WisolSMTLineApp.Model     5      9System&clr-namespace:System;assembly=mscorlib     5	        � 	Resources  5       �� 
TargetType �� Property �� Value   
enumValues S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName��%�   '��       &	  p     �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 16(�6)   5      S� 5   
   $ 	GetValues��60   "  6G    5      M� 5      $ model:WorkingMode��6   5      5      .��5      � 
�������$��50q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   .+�5&   
   �� $��LineID:��6   $��0��6#   �� 5'   
   $��1��6   $��0��6#   $��10q�60   
 �� SelectedIndex$
 0��6�   ��6<   �� *Lines+ �� SelectedItem 6Z   �� *SelectedLine+��5(      �� 5)      .R�5*      �� ��6!   �� *Name+5+      5-      �� 5/   
   $��1��6   $��Mode:��6!   �� 50   
   -   5        $��ModelComboBox��50   w   $��1��6   $��1��6#   $��10q�60   $
 0��6�   ��6<   ��  �� Source# [   6�   �� *SelectedWorkingMode+52      �� 54   
   $��Default Lots:��6   $��2��6)   �� 55   
   $��1��6   $��2��6"   $��10q�6/   ��6;   �� *DefaultLots+ �� Mode$ TwoWay=��� 57   
   $��Default Level:��6   $��3��6*   �� 58   
   $��1��6   $��3��6"   $��10q�6/   ��6;   �� *DefaultLevel+$ TwoWay=��� 5<   
   $
��Save��6   $��4��6    $��20q�6-   $��2��69   ��6M   �� *ClickCommand+5=      5>      