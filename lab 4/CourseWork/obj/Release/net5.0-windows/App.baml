   M S B A M L   `   `   `  ���� E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CourseWork
CourseWork      CourseWork.App   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:CourseWork       �� 	Resources     ComboBoxToggleButton �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius F�Name  ComboBoxTextBox �� 
TargetType �� Property �� Value	 �� 	ClickMode
 d� 	IsChecked �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation �� Triggers P� Property P� Value �� 
TargetName P� 
SourceName  �� Triggers! �� Padding 
 Yellow" �� Color  Dark 	 Light  	VeryLight  YellowButton  MiddleSizedButton# �� BasedOn  
Title Test(  "System.Windows.Controls.DatePicker$ �� 	Resources:  4System.Windows.Controls.Primitives.DatePickerTextBox% �� AncestorType& �� StringFormat=  7System.Windows.Controls.Primitives.DataGridColumnHeader&   System.Windows.Controls.DataGrid' �Name( � RowDefinitions) ]�Name* ]� Thumb+ n�Name, ]� IncreaseRepeatButton- ��Name. J� Opacity/ ]� DecreaseRepeatButton 	 ButtonSelectBrush 
 	DarkBrush0 �� Angle��%y  &	        &	 A    '�� }    7   7  '��     &	 t    &	 �    &	 �    &	 �    &	 �    7  7  7  7  7  &	 g
    7  '�� �
    7  &	 �
    7  '  �    7  7  '      7  7  '  z    7  7  7  7  'n� �    '�� Y    �� " d�.R��  �� �� $��20 �.+��� $ Border��$��2��$ 0�����	#FF3F3F3F���	#FF97A0A5$��1q��� $��0��$ 0��$��1q����	#FF3F3F3F���	#FF97A0A5$��0,0,1,0q�F� $ Arrow��$��1��
�������$��Center=�$��Center=�=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	�� " ��.R��� $ PART_ContentHost��$|�False��#��z���� " ��.���  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true����  � ��$
 Auto=���  � ��$
 Auto=���  � ��$
 true����  � ��$	 120����  � ��$ 20����  � ��
 �������  � �� �� " ��.R�� .+�d� $��ToggleButton��8��  $��2��$|�false��$	 Press=�
 �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $��ContentSite��$ False��#��z #��z #��z��$��3,3,23,3q�$��Center=�$
��Left=��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,3,23,3q�$
|�True�����	#FF3F3F3F
��� � �$y�Hidden=�# z ��Z� � $��Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $��DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����	#FF3F3F3F$��1q����#888888�� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$ 95��P�  � {�$ false��. ���  � �� �#888888P�  �@��
IsGrouping$
 true��. ���  � ��$ false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$ 0���� $ DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable$
 true��. ���  � ��$ false���� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=�  �� " ��.���  �@J�SnapsToDevicePixels$
 true����  � ��
 �������  �@�OverridesDefaultStyle$
 true����  � �� �� " ��.R��� $��Border��$! 2q�$
 true��._���  P�  �@��IsHighlighted$
 true��. ��� $ Border�� � �� �	#FF4F4F4FP�  � {�$ false��. ���  � �� �#888888�� $" #FFDD00���� $" #292F34���� $" #373E46���� $" #DCE0E4���� " ��.���  � ��8   ��  � ��$ 10(���  � ��$ Arial Rounded MT Bold)���  � �� �� " ��.R��� $ border��#��z��8��  ��  �� *TemplatedParent+$ FontSize�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� 8   � ����  � ��
 ������� $ border�� � ��$ 3q�P�  � ��$
 True��. ��� 8   � ���� 8   � ���� " ��8#   .���  � ��$ 80����  � ��$ 40���� " ��8#   .���  � ��$ 8(���  � ��$ 80���� " ��.���  � ��$ 20(���  � ��$ Bell MT)���  � ��$ Italic$���  � ��$
 Bold"���  �@��TextAlignment$ Center=���  � ��8   �� "  $ ���� '        "  .���  � �� �� .R��� 8��  8�� ���� $ SelectedDate� �� "%  $& 
yyyy-MM-dd���� "  .���  � ��8   ��  � ��8  ��  � ��$ 30����  � ��$ Center=��� "  .���  �@ HeadersVisibility$ Column=���  �@ CanUserResizeColumns$ False����  �@ AutoGenerateColumns$
 True����  � ��8   ��  � ��8  ��  �@ RowBackground8  $ ���� '��       " ��.���  �@��CornerRadius$ 5����  � ��8  �� " n�.���  � �� �� " n�.R�� $
' Grid��.+��� $��Stretch=�$��Stretch=�$��10��
������ �� $ 
Rectangle1��$ 2��$��Stretch=�$��Stretch=�#��z���� " ��.���  �@��IsFlicksEnabled$ false����  � �� �#8C8C8C��  � ��
 ���� ��  � ��$ 10����  � �� �� " ��.R�� $' GridRoot��$��8��#��z��( �� $��0.00001* �.+�]� $) 
PART_Track��$��0��$
��true��$|�false��* n� $+ Thumb��#��z��, �� $- PageUp��$��ScrollBar.PageDownCommand��$. 0Q�$|�false��/ �� $- PageDown��$��ScrollBar.PageUpCommand��$. 0Q�$|�false�� P� $ Thumb�� �@J�IsMouseOver$
 true��. ��� # � 	 $ Thumb�� � ��P� $ Thumb�� �@n�
IsDragging$
 true��. ��� # � 
 $ Thumb�� � ��P�  � {�$ false��. ��� $ Thumb�� � y�$ 	Collapsed=�P�  �@��Orientation$ 
Horizontal=�. ��� $ GridRoot�� �@�LayoutTransform �� $	0 -90Q��� $ 
PART_Track�� �@�LayoutTransform �� $	0 -90Q���  � ��$
 Auto����  � ��$ 8���� $ Thumb�� �@�Tag$ 
Horizontal���� $ PageDown�� � ��$ ScrollBar.PageLeftCommand���� $ PageUp�� � ��$  ScrollBar.PageRightCommand��