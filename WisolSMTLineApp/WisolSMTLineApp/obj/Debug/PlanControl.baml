   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5      =#clr-namespace:WisolSMTLineApp.ModelWisolSMTLineApp.Model  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      !    WisolSMTLineApp.PlanControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WisolSMTLineApp     5      5model#clr-namespace:WisolSMTLineApp.Model     5      6sys&clr-namespace:System;assembly=mscorlib     5        � 	Resources  5       �� 
TargetType �� Property �� Value��%z   '��       �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 16(�6)   5      5      .��5      � 
�������$��50q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��LineID:��6   $��0��6#   �� 5    
   $��1��6   $��0��6$   $��10q�61   ��6=   ��  �� Path$ SelectedLine.Name��� 5"   
   $��1��6   $��Date:��6"   (  "System.Windows.Controls.DatePicker  5#   
   $��1��6   $��1��6#   $��Center=�63   $��10q�6N      SelectedDate 5$      �� *SelectedDate+ �� Mode$ TwoWay=��� 52   
   $��Model:��6   $��3��6"   �� 53   
   -   5        $��ModelComboBox��56      $��1��53      $��3��6#   $��10q�60   ��54      �� *
Products+	 �� SelectedItem	 55      �� $ SelectedProduct�$ TwoWay=���57      �� 58      .R�59      �� ��6    �� *Name+5:      5<      �� 5>   
   $��Remain Nodes:��6   $��4��6)   �� 5?   
   $��1��6   $��4��6"   $��10q�6/   ��6;   �� *RemainNodes+�� 5B   
   $��Create��6   $��5��6"   $��2��6/   $
��Left=�5C      $	��100��6,   $��Center=�5D   2   ��6   �� *ClickCommand+62   5E      5F      