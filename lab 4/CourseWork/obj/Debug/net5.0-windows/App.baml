   M S B A M L   `   `   `  ����E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CourseWork
CourseWork  5          CourseWork.App   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      *localclr-namespace:CourseWork     5        �� 	Resources  5         ComboBoxToggleButton �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius F�Name  ComboBoxTextBox  StyleForItem �� 
TargetType �� Property �� Value	 �� Padding
 J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  DarkComboBox �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency � ActualWidth �� MaxDropDownHeight K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation  P� 
SourceName! �� Triggers 
 Yellow" �� Color  Dark 	 Light  	VeryLight  YellowButton 	 MiddleSizedButton# �� BasedOn 
 
Title Test(  "System.Windows.Controls.DatePicker$ �� 	Resources:  4System.Windows.Controls.Primitives.DatePickerTextBox% �� AncestorType& �� StringFormat=  7System.Windows.Controls.Primitives.DataGridColumnHeader&   System.Windows.Controls.DataGrid' �Name( � RowDefinitions) ]�Name* ]� Thumb+ n�Name, ]� IncreaseRepeatButton- ��Name. J� Opacity/ ]� DecreaseRepeatButton  ButtonSelectBrush  	DarkBrush0 �� Angle��%�   &	        &	     &	 �    &	 �    7  7   7  &	     &	 7    &	 X    &	 y    &	 �    7  7  7  7  7  &		 v    7  '�� �    7  &	
 W    7  '  l    7  7  '  �    7  7  '  1    7  7  7  7  'n�     '�� _    �� 5	   
   " d�67   .R�5
      �  5      �� 5      �� 5      $��20 �6+   .+�5      �� $ Border��5      $��2��5      $ 0��5      ���	#FF3F3F3F���	#FF97A0A5$��1q�5      �� 5      $��0��5      $ 0��5      $��1q�5      ���	#FF3F3F3F���	#FF97A0A5$��0,0,1,0q�5      F� 5      $ Arrow��5      $��1��5      
�������$��Center=�5!      $��Center=�5"      =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	5#      5%      5&      �� 5(      " ��66   .R�5)      �� $ PART_ContentHost��6   $|�False��63   #��z��6E   5*      �� 5-      " ��6)   .�5.      ��  �@J�SnapsToDevicePixels$
 true��68   �� 5/       � ��
 �����6/   �� 50       �@�OverridesDefaultStyle$
 true��6:   �� 51       � �� 52      �� 53      " ��6*   .R�54      �� $��Border��6%   $	 2q�55   '   $

 true��56   '   ._�57   "   �� 58       59      P� 5:   "    �@��IsHighlighted$
 true��6C   . �5;   &   �� $ Border��6-    � �� �	#FF4F4F4F6W   5<   #   P� 5=   "    � {�$ false��6?   . �5>   &   ��  � �� �#8888886C   5?   #   5A      5C      5D      �� 5F      " ��6)   .�5G      ��  �@J�SnapsToDevicePixels$
 true��64   �� 5H       �@�OverridesDefaultStyle$
 true��66   �� 5I       � ��$
 Auto=�6K   �� 5J       � ��$
 Auto=�6I   �� 5K       � ��$
 true��6>   �� 5L       � ��$	 120��6)   �� 5M       � ��$ 20��6*   �� 5N       � ��
 �����6+   �� 5O       � �� 5P      �� 5Q      " ��6&   .R�5R      � .+�5S      �� 8��  6+   d� 5T      $��ToggleButton��5U      8�� $��2��5W      $|�false��5X      $ Press=�5Z       5Y      �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5[      �� 5\      $��ContentSite��6/   $ False��6B   #��z 6\   #��z 5]      #��z��5^      $��3,3,23,3q�5_      $��Center=�5`      $
��Left=�5a      �� 5b      $ PART_EditableTextBox��6&   8�� $
��Left=�5e      $��Center=�5f      $��3,3,23,3q�5g      $
|�True��5h      ���	#FF3F3F3F
��� � �$y�Hidden=�5k      # z 5l      ��5c      Z� 5l      � 5m      $��Popup��5n      $��Bottom=�5o      #��z 5p      $
 True��5q      $|�False��5r      $��Slide=�5s      .��5u   "   � $��DropDown��6'   $

 True��5v      #��z 5w      #��z 5x      .+�5y   &   �� $ DropDownBorder��5z   !   ���	#FF3F3F3F$��1q�5}   !   ���#8888885~   !   �� 5   &   $��4,6,4,6q�63   $

 True��6D   .��5�   *   �� $
 True��65   $ 	Contained=�6H   5�   '   5�   #   5�      5�       5�      P� 5�       �@��HasItems$ false��6:   . �5�   "   �� $ DropDownBorder��6)    � ��$ 95��6Z   5�      P� 5�       � {�$ false��6;   . �5�   "   ��  � �� �#8888886?   5�      P� 5�       �@��
IsGrouping$
 true��6<   . �5�   "   ��  � ��$ false��6R   5�      P� 5�      $  Popup��6&    �@�AllowsTransparency$
 true��6]   . �5�   "   �� $ DropDownBorder��6)    �@��CornerRadius$ 0��6]   �� 5�   "   $ DropDownBorder��6)    � ��$ 0,2,0,0q�6W   5�      P� 5�       �@��
IsEditable$
 true��6=   . �5�   "   ��  � ��$ false��6>   �� 5�   "   $ PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5�   "   $ ContentSite��6)    � y�$ Hidden=�6X   5�      5�      5�      ! 5�      5�      �� 5�   
   $" #FFDD00��6-   �� 5�   
   $" #292F34��6-   �� 5�   
   $" #373E46��6-   �� 5�   
   $" #DCE0E4��6-   �� 5�   
   " ��6%   .�5�      ��  � ��8   5�      �� 5�       � ��$ 10(�6)   �� 5�       � ��$ Arial Rounded MT Bold)�6+   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �� $ border��6!   #��z��5�   !   8��  5�   !   ��  �� *TemplatedParent+$ FontSize�._�5�      �� $��Center=�6/   $��Center=�6L   5�       5�      P� 5�       �@J�IsMouseOver$
 True��6=   . �5�   "   �� 8   � ��6?   �� 5�   "    � ��
 �����6?   �� 5�   "   $ border��6D    � ��$ 3q�6X   5�      P� 5�       � ��$
 True��6;   . �5�   "   �� 8   � ��6?   �� 5�   "   8   � ��6?   5�      5�      5�      5�      �� 5�   
   " ��6*   8#   .�5�      ��  � ��$ 80��6&   �� 5�       � ��$ 40��6'   5�      �� 5�   
   " ��6   8#   .�5�      ��  � ��$ 8(�6)   �� 5�       � ��$ 80��6&   5�      �� 5�   
   " ��6#   .�5�      ��  � ��$ 20(�6)   �� 5�       � ��$ Bell MT)�6+   �� 5�       � ��$ Italic$�6*   �� 5�       � ��$
 Bold"�6+   �� 5�       �@��TextAlignment$ Center=�6.   �� 5�       � ��8   6+   5�      �� 5�   
   "  6   $ 5�      ���� 5�      '        "  6   .�5�      ��  � �� 5�      �� 5�      .R�5�   "   �� 8��  8�� ��5�   *   �� $ SelectedDate� �� "%  $& 
yyyy-MM-dd��5�   *   5�      5�      5�      5�      5�      �� 5�   
   "  6   .�5�      ��  � ��8   6+   �� 5�       � ��8  6+   �� 5�       � ��$ 30��6'   �� 5�       � ��$ Center=�6;   5�      �� 5   
   "  6   .�5     ��  �@ HeadersVisibility$ Column=�62   �� 5      �@ CanUserResizeColumns$ False��65   �� 5      �@ AutoGenerateColumns$
 True��64   �� 5      � ��8   6+   �� 5      � ��8  6,   �� 5      �@ RowBackground8  6.   $ 5     ���� 5     '��       " ��6   .�5	     ��  �@��CornerRadius$ 5��65   �� 5
      � ��8  64   5     5     5     �� 5  
   " n�6   .�5     ��  � �� 5     �� 5     " n�6&   .R�5     � $
' Grid��6   .+�5     �� $��Stretch=�6(   $��Stretch=�6F   $��10��6b   
������ 6m   �� 5     $ 
Rectangle1��6%   $ 2��69   $��Stretch=�6J   $��Stretch=�6h   #��z��6�   5     5     5     5     �� 5  
   " ��6   .�5     ��  �@��IsFlicksEnabled$ false��5      �� 5!      � �� �#8C8C8C5"     �� 5#      � ��
 ���� 5$     �� 5%      � ��$ 10��5&     �� 5'      � �� 5(     �� 5)     " ��6&   .R�5*     � $' GridRoot��6   $��8��5+     #��z��5,     ( 5-     �� 5.  "   $��0.00001* �60   .+�50     ]� $) 
PART_Track��6$   $��0��51      $
��true��52      $|�false��53      * 54  "   n� 55  &   $+ Thumb��6,   #��z��56  (   , 58  "   �� 59  &   $- PageUp��63   $��ScrollBar.PageDownCommand��5:  /   $. 0Q�5;  /   $|�false��5<  /   / 5>  "   �� 5?  &   $- PageDown��63   $��ScrollBar.PageUpCommand��5@  /   $. 0Q�5A  /   $|�false��5B  /   5D     5E      5F     P� 5G     $  Thumb��6&    �@J�IsMouseOver$
 true��5I  "   . �5J  "   �� # �  6)   $ Thumb��5K  %    � ��5L  %   5M     P� 5N     $  Thumb��6&    �@n�
IsDragging$
 true��5P  "   . �5Q  "   �� # �  6)   $ Thumb��5R  %    � ��5S  %   5T     P� 5U      � {�$ false��5V  "   . �5W  "   �� $ Thumb��6)    � y�$ 	Collapsed=�5Y  %   5Z     P� 5[      �@��Orientation$ 
Horizontal=�5\  "   . �5]  "   �� $ GridRoot��6)    �@�LayoutTransform 5_  &   �� 5`  *   $	0 -90Q�6:   5b  #   �� 5c  "   $ 
PART_Track��6)    �@�LayoutTransform 5e  &   �� 5f  *   $	0 -90Q�6:   5h  #   �� 5i  "    � ��$
 Auto��5j  %   �� 5k  "    � ��$ 8��5l  %   �� 5m  "   $ Thumb��6)    �@�Tag$ 
Horizontal��5o  %   �� 5p  "   $ PageDown��6)    � ��$ ScrollBar.PageLeftCommand��5r  %   �� 5s  "   $ PageUp��6)    � ��$  ScrollBar.PageRightCommand��5u  %   5v     5x     5z     5{     5}     5~     