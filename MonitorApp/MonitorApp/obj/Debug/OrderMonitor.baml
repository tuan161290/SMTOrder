   M S B A M L   `   `   `  ����E  AMonitorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MonitorApp
MonitorApp  5      ;"clr-namespace:MonitorApp.ConverterMonitorApp.Converter  5          MonitorApp.OrderMonitor   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:MonitorApp     5      8	converter"clr-namespace:MonitorApp.Converter     5        � 	Resources  5
      /   )MonitorApp.Converter.TextToColorConverter   TextToColorConverter��%   &	          5   
   6)   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $
��10,5q�6   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67    � MappingMode$ RelativeToBoundingBox=�6F   .��5      �  � Color$ White��6#    � Offset$	 0.3Q�61   � 5      $ 	SteelBlue��6#   $ 1Q�65   5      .+�5      �� $��
SMT ORDERS��6   $��84,0,0,0q�6*   $��24(�6<   
�����F�6J   �� 5      $��DemiBold"�6   $��0,0,5,0q�6.   $��18(�6?   $��Bottom=�6M   $��Right=�6h   �� 5      
��� � �$�T��6*    �� 5      
����  �$�F��6)    �� 5      
���   �$�S��6*    �� 5      
�������$� VINA��6*   5       �� 5!      $��10,0,0,0q�6   $��./Assert/Wisol.png��6&   $��64��6B   $
��Left=�6M   5"      �� 5#   
   $��1��6   $
��10,5q�6    ��6.   �� *UnconfirmOrders+��5$      �� 5%       �� 
TargetType" ��6   .�5&      ��  �� Property � ��	 �� Value$	 0q�68   �� 5'       � ��	 5(      �� 5)      " ��6$   .�5+   "   ��  � |�	 .  6>   �� 5,   "    � ��$	 Stretch=�6O   �� 5-   "    � ��$	 Stretch=�6H   �� 5.   "    � ��$	 5q�6;   �� 5/   "    � ��$	 -1q�6<   �� 50   "    � ��$	 Stretch=�6F   �� 51   "    � ��$	 Stretch=�6M   52      54      55      ��57      �� 58      .R�59      �� $��2q�6   ��5:      �� *Reason+
 �� 	Converter#
 [   �� ConverterParameter$ border��._�5<      �  � ColumnDefinitions 5=      �� 5>   "   �� 5?   "   �� 5@   "   �� 5A   "   $��3* �63   .+�5C      �� $��1q�6%   
�������._�5D   "   �� �� 5E      $�LINE:��6"    �� 5F      
�����F��6"   �� *LineInfo.Name+6A   5G   #   5H      �� 5I      $��1��6%   $��0,1,1,1q�65   
�������._�5J   "   �� �� 5K      $�MODEL:��6"    �� 5L      
�����F��6"   �� *Product.Name+6@   5M   #   5N      �� 5P      $��2��6%   $��0,1,1,1q�65   
�������._�5Q   "   �� �� 5R      $�Amount:��6"    �� 5S      
�����F��6"   �� *Amount+6:   5T   #   5U      �� 5W      $��3��6%   $��0,1,1,1q�65   
�������._�5X   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5Y   &   �� -   5        $��OK��5Y   -   $��5q�6:   $	��120��6Z   
 � Tag$ ok��5Z   -   ��5[   -   �� *Reason+#
 [  $ ok���� 5\   &   -   5        $��JIG SHORTAGE��5\   -   $��5q�6Y   $ jig-shortage��5]   -   ��5^   -   �� *Reason+#
 [  $ jig-shortage���� 5_   &   -   5        $��MGZ SHORTAGE��5_   -   $��5q�6Y   $ magazin-shortage��5`   -   ��5a   -   �� *Reason+#
 [  $ magazin-shortage���� 5b   &   -   5        $��PLM WAITING��5b   -   $��5q�6X   $ plasma-waiting��5c   -   ��5d   -   �� *Reason+#
 [  $ plasma-waiting���� 5e   &   -   5        $��PCB SHORTAGE��5e   -   $��5q�6Y   $ pcb-shortage��5f   -   ��5g   -   �� *Reason+#
 [  $ pcb-shortage��5h   #   5i      5j      5k      5l      5n      r� 5o   
   -   5        $��myMediaElement��5o      $��3��6-    r� Source$ 
Melody.mp3?�6:   $y�	Collapsed=�6N    r� LoadedBehavior$ Manual=�5p   
    r� UnloadedBehavior$
 Stop=�6"    r� Stretch$
 Fill=�6:   5q      5r      