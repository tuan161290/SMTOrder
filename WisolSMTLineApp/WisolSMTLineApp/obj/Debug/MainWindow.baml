   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5   	        WisolSMTLineApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WisolSMTLineApp     5   	     � Title$  Wisol SMT Order��5   	   $	��600��6!   $	��800��6.    � 	Resources 5	       �� 
TargetType �� Property �� Value��%R   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 16(�6)   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67    � MappingMode$ RelativeToBoundingBox=�6F   .��5      � 	 � Color$	 White��6#   
 � Offset$	
 0.3Q�61   � 5      $	 	SteelBlue��6#   $
 1Q�65   5      .+�5      �� $��DemiBold"�6   $��0,0,5,0q�6.   $��18(�6?   $��Bottom=�6M   $��Right=�6h   �� 5      
��� � �$�T��6*    �� 5      
����  �$�F��6)    �� 5      
���   �$�S��6*    �� 5      
�������$� VINA��6*   5      �� 5       $��10,0,0,0q�6   $��./Assert/Wisol.png��6&   $��64��6B   $
��Left=�6M   �� 5!      $��Center=�6   
�����F�$��80,0,0,0q�6J   $��28(�6\   �� 5"      $�LINE:��6    �� 5#      �6   ��  �� Path$
 Name�   WisolSMTLineApp.Setting   SelectedLine �� Source# Z 5$      5%      �� 5&   
   -   5        $��MainTab��5&      $��1��6$    5'       �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%J  '��       �� 5(      " ��6   .�5)      ��  � �� 5*      �� 5+      " ��6.   .R�5,   "   �� $��Border��6)   $��1,1,1,0q�67   
�������$ 2,2,0,0��6i   $	��1,0q�6�   ._�5-   &   �� $ ContentSite��67   $��Center=�5.   )   $��Center=�5/   )   $��Header��50   )   $
��10,5q�51   )   52   #    53   "   P� 54   &    �@��
IsSelected . . �55   *   �� $ Border��61    � ��
 ��·�6[   56   '   P� 57   &    �@��
IsSelected .  . �58   *   �� $ Border��61    � ��
 �����6[   59   '   5;      5=      5>      5?      .@�5F      �� -   5        $��
MonitorTab��5F   5   $��MONITOR��6   $��16(�6'   .��5G      (� .��5H      $   WisolSMTLineApp.MonitorControl  $��Stretch=�6+   5I      5J      �� 5O      $��SETTING��6   $��16(�6'   .��5P      $   WisolSMTLineApp.SettingControl  5Q      5R      5S      5T      