   M S B A M L   `   `   `  ����E  AMonitorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MonitorApp.MasterPagesMonitorApp.MasterPages  5      *    $MonitorApp.MasterPages.ProductMaster   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:MonitorApp.MasterPages     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $
��10,5q�6   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67    � MappingMode$ RelativeToBoundingBox=�6F   .��5      �  � Color$ White��6#    � Offset$	 0.3Q�61   � 5      $ 	SteelBlue��6#   $ 1Q�65   5      .+�5      �� $��Model Master��6   $��84,0,0,0q�6,   $��24(�6>   
�����F�6L   �� 5      $��DemiBold"�6   $��0,0,5,0q�6.   $��18(�6?   $��Bottom=�6M   $��Right=�6h   �� 5      
��� � �$�T��6*    �� 5      
����  �$�F��6)    �� 5      
���   �$�S��6*    �� 5      
�������$� VINA��6*   5      �� 5      $��10,0,0,0q�6   $��../Assert/Wisol.png��6&   $��64��6C   $
��Left=�6N   5      �� 5   
   $��1��6   $
��10,5q�6    ��6.   �� *UnconfirmOrders+��5       �� 5!       �� 
TargetType" ��6   .�5"      ��  �� Property � �� �� Value$ 0q�68   �� 5#       � �� 5$      �� 5%      " ��6$   .�5'   "   ��  � |� .  6>   �� 5(   "    � ��$ Stretch=�6O   �� 5)   "    � ��$ Stretch=�6H   �� 5*   "    � ��$ 5q�6;   �� 5+   "    � ��$ -1q�6<   �� 5,   "    � ��$ Stretch=�6F   �� 5-   "    � ��$ Stretch=�6M   5.      50      51      ��53      �� 54      .R�55      �� $��2q�6   ._�56      � 	 � ColumnDefinitions	 57      �� 58   "   �� 59   "   �� 5:   "   �� 5;   "   $��3* �63   .+�5=      �� $��1q�6%   
�������._�5>   "   �� �� 5?      $	�ID:��6"    �� 5@      
�����F��6"   �� *LineInfo.Name+6A   5A   #   5B      �� 5C      $��1��6%   $��0,1,1,1q�65   
�������._�5D   "   �� �� 5E      $�NAME:��6"    �� 5F      
�����F��6"   �� *Product.Name+6@   5G   #   5H      �� 5J      $��3��6%   $��0,1,1,1q�65   
�������._�5K   "   �� 
 �� Orientation$
 
Horizontal=�6-   .+�5L   &   �� -   5        $
��Edit��5L   -   $��5q�6<   $	��120��6\   
 � Tag$ ok��5M   -   �� 5N   &   -   5        $��Delete��5N   -   $��5q�6S   $ jig-shortage��5O   -   5P   #   5Q      5R      5S      5T      5V      r� 5W   
   -   5        $��myMediaElement��5W      $��3��6-    r� Source$ 
Melody.mp3?�6:   $y�	Collapsed=�6N    r� LoadedBehavior$ Manual=�5X   
    r� UnloadedBehavior$
 Stop=�6"    r� Stretch$
 Fill=�6:   5Y      5Z      