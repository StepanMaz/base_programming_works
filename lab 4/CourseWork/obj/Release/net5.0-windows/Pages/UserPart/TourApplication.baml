   M S B A M L   `   `   `  ���� E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:CourseWork.Pages.UserPartCourseWork.Pages.UserPart  /    )CourseWork.Pages.UserPart.TourApplication   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:CourseWork.Pages.UserPart       N� Title$  TourApplication�� � 	Resources ��%    .��� .+�� $	��380��$	��300��$��	0,10,30,0q�$��Right=�$	��Top=�.+�� $	��300��$��70��$��Right=�$	��Top=�.+��� $��Ціна до:��$
��Wrap=�$	��120��$��25��$��	170,5,0,0q� �� TextAlignment$ Center=�$	��Top=�$
��Left=��� $��Ціна від:��$
��Wrap=�$	��120��$��25��$��10,5,0,0q�$ Center=�$	��Top=�$
��Left=��� -    ��Name$ ButtomPrice��$��0�� �� TextWrapping$
 Wrap=�$	��120��$��25��$	��Top=�$
��Left=�$��	10,35,0,0q��� -   $ TopPrice��$��99999��$
 Wrap=�$	��120��$��25��$	��Top=�$
��Left=�$��
170,35,0,0q��� $
��Wrap=�$��40��$��0,33,0,0q�$ Center=�$��20(�$��25��$	��Top=�$��Center=���  � Language$ uk-ua�$	�─��� $	��300��$��70��$��Right=�$	��Top=�$��0,70,0,0q�.+��� $��Дата до:��$
��Wrap=�$	��120��$��25��$��	170,5,0,0q�$ Center=�$	��Top=�$
��Left=��� $��Дата від:��$
��Wrap=�$	��120��$��25��$��10,5,0,0q�$ Center=�$	��Top=�$
��Left=�(  "System.Windows.Controls.DatePicker  -     Name$ 	StartDate��   SelectedDate 
2019-01-01$	��120��$��25��$��	10,35,0,0q�$
��Left=�$	��Top=�  -   $ EndDate�� 
2029-01-01$	��120��$��25��$��	0,35,10,0q�$��Right=�$	��Top=��� $
��Wrap=�$��40��$��0,35,0,0q�$ Center=�$��20(�$��25��$	��Top=�$��Center=��� $ uk-ua�$	�─���� $	��160��$	��160��$��Center=�$	��Top=�$��	0,140,0,0q�.���� -    ��Name$ CountryFilter��   
CreamColor#��[  $��Stretch=�$��Stretch=�$��0,0,0,0q�.+��� -   	 ��Name$	 	AddButton��$��25��$��20(�$	��Top=�$��Center=�$��25������ 
 �� ImageSource$"
 /Pages/UserPart/addimage.png��  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 15���� $��40��$��	0,310,0,0q�$	��230��$
��Auto=�$��Hidden=�$��Right=�$	��Top=�.���� -   $ TravelWaysFilter��#��[   �� Orientation$ 
Horizontal=�.+��� -   $	 AddTravelWay��$��25��$��20(�$	��Top=�$��Center=�$��25������ $"
 /Pages/UserPart/addimage.png�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 15���� -	   $��Викл��$��10(�$��40��$��40��  MiddleSizedButton#��[ $��20,310,0,30q�$
��Left=�$	��Top=�&   System.Windows.Controls.DataGrid  -
     Name$ 
ToursTable��   
IsReadOnly$
 True��$	��400��$	��180��$��Center=�$
��Left=�$��35,0,0,0q��� $��Немає турів��$
��Wrap=�$y�Hidden=�  
Title Test#��[ $��35,135,365,135q�$	��160��$��30��