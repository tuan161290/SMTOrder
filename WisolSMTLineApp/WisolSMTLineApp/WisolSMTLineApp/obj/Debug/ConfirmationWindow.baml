   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5   	   E'clr-namespace:WisolSMTLineApp.ViewModelWisolSMTLineApp.ViewModel  5   	   (    "WisolSMTLineApp.ConfirmationWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WisolSMTLineApp     5   	   6VM'clr-namespace:WisolSMTLineApp.ViewModel     5   	   $	��400��5	      $	��400��6'   .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Unconfirmed Orders��6   $��0��6.   � 5   
   $��1��6   $��3��6   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��LstOrderNotFinish��5         CanUserAddRows .     AutoGenerateColumns .  ��5      �� *LstOrderNotFinish+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Amount��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6f   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ��  �� Path$ Amount�	 �� Mode$	 TwoWay=�
   ElementStyle
 5      �� 5       �� 
TargetType" ��6$   .�5    "   ��  �� Property � �� �� Value$ Center=�6H   �� 5!   "    � ��$ Center=�6F   5"      5$      4  .System.Windows.Controls.DataGridTemplateColumn  5%      $ Action��6-   $
 1.5* 6=      CellTemplate 5&      �� 5'      .R�5(   "   �� $��1q�6)   $��Confirm��5)   !   ��5*   !   ��  �� ElementName$ LstOrderNotFinish��$ DataContext.ClickCommand���5+   A   �� *.+5,      5.      50      5H      �� 5I   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��2��6;   .+�5J      �� $��
New Order:��6   $��Center=�6*   �� 5K      $	��130��6.   $��10q�6:   ��6   �� *Amount+6:   5L      �� 5N   
   $��Create��6   $��3��6"   $��5q�6/   ��6:   �� *SubmitCommand+5P      5Q      