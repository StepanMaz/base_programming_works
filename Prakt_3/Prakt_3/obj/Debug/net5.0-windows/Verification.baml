   M S B A M L   `   `   `  ����B  >Prakt_3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Prakt_3Prakt_3  5   	       Prakt_3.Verification   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Prakt_3     5   	     � Title$  Verification��5   	   $	��320��6   $	��300��6+    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5
      � .+�5   
   �� -   5         ��Name$ Login��5       �� TextWrapping$
 Wrap=�6!   $��	0,75,10,0q�65   $	��150��6H   $��16(�6T   $��Right=�6�   $	��Top=�6�   $��30��6�   �� 5   
   $
��Wrap=�6   $��	10,75,0,0q�6(   $��16(�6;   $��30��6I   $
��Left=�6U   $	��Top=�6p   $	��110��6�   �� 6�   $�Введіть логін��6�   �� 6�    � Language$ uk-ua�6�   $�:��6�   6�   �� 5   
   $
��Wrap=�6   $��
10,140,0,0q�6(   $��16(�6<   $��30��6J   $	��120��6V   $
��Left=�6b   $	��Top=�6}   �� 6�   $�Введіть ��6�   �� 6�   $ uk-ua�6�   $�пароль��6�   �� 6�   $�:��6�   6�   �� 5   
   $
��Wrap=�6   $��
10,10,10,0q�6(   $	��280��6<   $��40��6H   $��Center=�6T   $	��Top=�6q   $��22(�6�    �� TextAlignment$ Center=�6�   $
��Bold"�6�   $��Italic$�6�   �� 6�   $ uk-ua�6�   $0�*Підтвердіть свою особу��6�   6
  �� 5   
   -   5         ��Name$ TriesAmount��5      $&�� Кількість спроб: 3��6%   $��
10,197,0,0q�6B   $��Italic$�6V   $��30��6i   $	��130��6u   $
��Left=�6�   $	��Top=�6�   G� 5   
   -   5         G�Name$ Password��5      $	��150��6(   $��30��64   $��
0,140,10,0q�6@   	 G� Password$	  ��6T   $	��Top=�6�   $��Right=�6�   �� 5   
   $����Коли кількість спроб досягне '0' дане програмне забезпечення буде закрите��6   $
��Wrap=�6e   $��10,227,10,10q�6y   $��Italic$�6�   
 J� Opacity$	
 0.8Q�6�   $��
Sitka Text)�6�   $��Stretch=�6�   $��Bottom=�6�   $ Center=�6  �� 5   
   -   5         ��Name$ EnterButton��5      ${�False��6&   $��
Війти��68   $��175,197,0,0q�6H   $��80��6]   $��30��6h   $
��Left=�6�   $	��Top=�6�   5      5      