   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5      $    WisolSMTLineApp.MonitorControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WisolSMTLineApp     5        � 	Resources  5	       �� 
TargetType �� Property �� Value��%z   '��       �� 5
   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 20(�6)   5      5      .��5      � 
�������$��20q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Status:��6   �� 5   
   $��1��62   $��10q�6B   ��6   �� *ClickCommand+��6N   �� *	Ellipse+.��5       �� ��6   �� *WorkingStatus+5!      �� 5"   
   $��Model:��6   $��1��6"   �� 5#   
   $��1��6   $��1��6$   $��10q�61   $��Center=�6[   ��6=   �� *Product.Name+5%      �� 5'   
   $��Odered Node:��6   $��2��6(   �� 5(   
   $��1��6   $��2��6$   $��10q�61   $��Center=�6b   ��6=   ��  �� Path$ PlanView.order�6b   �� 5*   
   $��Elapsed:��6   $��3��6$   �� 5+   
   $��1��6   $��3��6$   $��10q�61   $��Center=�6d   ��6=   �� $ PlanView.elapsed�6d   �� 5-   
   $��Remain:��6   $��4��6#   �� 5.   
   $��1��6   $��4��6$   $��10q�61   $��Center=�6c   ��6=   �� $ PlanView.remain�6c   �� 50   
   $��Order Status:��6   $
��Wrap=�6)   $��5��6=   �� 51   
   $��1��6   $��5��6$   $��Center=�6P   ��61   �� *OrderDuration+6P   �� 53   
   $��Action:��6   $
��Wrap=�6#   $��6��67   �� 54   
   $��1��6   $��10q�6"   $��6��6O   ��6.   �� *OrderCommand+.��55      �� $��1��6   $��5��6(   $��Confirm��65   $��Center=�6D   56      57      58      