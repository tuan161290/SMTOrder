   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5      $    WisolSMTLineApp.MonitorControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WisolSMTLineApp     5        � 	Resources  5	       �� 
TargetType �� Property �� Value��%z   '��       �� 5
   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 20(�6)   5      5      .��5      � 
�������$��20q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��340 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Status:��6   � 5    
   $��1��6   $��10q�6    5!      �� 5"      �� 5#      $
��Auto �6#   .+�5%      �� ��6   �� *ClickCommand+��66   �� *	Ellipse+.��5&      �� ��6   �� *WorkingStatus+5'      �� 5(      $��StopLine��65   
����p��$��1��6c   $��Right=�6s   ��6   �� *StopCommand+6s   5)      �� 5+   
   $��Model:��6   $��1��6"   �� 5,   
   $��1��6   $��1��6$   $��10q�61   $��Center=�6[   ��6=   �� *Product.Name+5.      �� 50   
   $��Odered PCB:��6   $��2��6'   �� 51   
   -   5        $��1��51      $��2��53      $��10q�6!   $��Center=�6T   ��6-   ��  �� Path$ PlanView.Ordered�6T   �� 55   
   $��Elapsed PCB:��6   $��3��6(   �� 56   
   $��1��6   $��3��6$   $��10q�61   $��Center=�6d   ��6=   �� $ PlanView.Elapsed�6d   �� 58   
   $��Remain PCB:��6   $��4��6'   �� 59   
   $��1��6   $��4��6$   $��10q�61   $��Center=�6c   ��6=   �� $ PlanView.Remain�6c   �� 5;   
   $��Order Status:��6   $
��Wrap=�6)   $��5��6=   �� 5<   
   $��1��6   $��5��6$   $��Center=�6P   ��61   �� *OrderDuration+6P   �� 5>   
   $��Action:��6   $
��Wrap=�6#   $��6��67   �� 5?   
   $��1��6   $��10q�6"   $��Confirm��6O   $��6��6a   $��Stretch=�6n   ��6.   �� *OrderCommand+6n   �� 5C   
   $��Solder:��6   $
��Wrap=�6#   $��7��67   � 5D   
   $��1��6   $��10q�6    $��7��6,    5E      �� 5F      �� 5G      �� 5H      .+�5J      �� $��Solder Input��6   ��6-   �� *CreateFluxOrderCommand+�� 5L      $��1��6   $	��5,1q�6)   $��2��66   $��16(�6J   �� 5M      �6   �� *$"PlanView.FluxOrder.FLuxOrderStatus+ �� 5N      �6   �� *" PlanView.FluxOrder.TotalDuration+ �� StringFormat$ {0:hh\:mm\:ss}��5O      5R      5S      5T      