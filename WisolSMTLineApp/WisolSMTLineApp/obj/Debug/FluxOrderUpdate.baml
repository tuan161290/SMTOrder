   M S B A M L   `   `   `  ����J  FWisolSMTLineApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WisolSMTLineAppWisolSMTLineApp  5   	   %    WisolSMTLineApp.FluxOrderUpdate   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WisolSMTLineApp     5   	     � Title$  FluxOrderUpdate��5   	   $	��450��6!   $	��600��6.    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       '�� H     �� 5
   
   " ��6   .�5      ��  � ��$ 18(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 18(�6)   5      5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��24(�6   $��SemiBold"�6#   �� 5      $�Solder Order��6    �� 5       �6   ��  �� Path$
 Name�   WisolSMTLineApp.Setting   SelectedLine	 �� Source#	 Z 5!      �� 5#   
   $��LotID:��6   $��1��6"   $��10q�6/   �� 5$   
   $��1��6G   $��1��6W   $��10q�6d   ��6   �� *CurrentFluxOrder.LotID+
 �� Mode$
 TwoWay=�6d   �� 5&   
   $��Time Defrosting:��6   $��2��6,   $��10q�69   �� 5'   
   $��1��6S   $��2��6c   $��10q�6p   ��6   �� *#!CurrentFluxOrder.DefrostTimeStamp+$
 OneTime=�6p   �� 5)   
   $��Time Sending :��6   $��3��6*   $��10q�67   �� 5*   
   $��1��6V   $��3��6f   $��10q�6s   ��6   �� *&$CurrentFluxOrder.SendToLineTimeStamp+$
 OneTime=�6s   �� 5,   
   $��Time Input :��6   $��4��6(   $��10q�65   �� 5-   
   $��1��6L   $��4��6\   $��10q�6i   ��6   �� *CurrentFluxOrder.LineInput+$
 OneTime=�6i   �� 5/   
   -   5        $��Submit��5/      $��5��6"   $��10q�6/   6;   �� 50   
   -   5        $��Close��50      $��5��6!   $��1��6.   $��10q�6>   
���  ��6[   51      52      