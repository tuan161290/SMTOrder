   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5      "    WisolSMTLineApp.OrderControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WisolSMTLineApp     5      :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5        � 	Resources  5
       �� 
TargetType �� Property �� Value��%z   '��       �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 16(�6)   5      5      .��5      � 
�������$��50q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      .+�5   
   �� $��Model:��6   $��0��6"   �� 5    
   $��1��6   $��0��6$   $��10q�61   ��6=   �� *Product.Name+5!      �� 5#   
   $��Amount:��6   $��1��6#   �� 5$   
   $��1��6   $��1��6#   $��10q�60   ��6<   �� *Amount+ �� Mode$ TwoWay=��� 5&   
   $��Order��6   $��2��6!   $��20q�6.   $��Center=�6:   $��2��6U   ��5'      �� *OrderCommand+5(      5)      