   M S B A M L   `   `   `  ����?  ;LZSS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LZSSLZSS  5   	       LZSS.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:LZSS     5   	   o kTelerik.Windows.Controls.Charting, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o	 kTelerik.Windows.Controls.GridView, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q
 mTelerik.Windows.Controls.Navigation, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
 5   	     � Title$
  LZSS��5	   	   $	��450��6   $	��525��6#   .��5
      � .+�5   
   (  "Telerik.Windows.Controls.RadButton  -   5          Name$ Przeglądaj��5   1   $��Przeglądaj��6   $
��Left=�6E   $��37��6`   $��263,204,0,0q�6l   $	��Top=�6�   $��85��6�   6�     5   
   -   5        $ 	Kompresuj��5   0   $��	Kompresuj��6   $
��Left=�6C   $��37��6^   $��
84,365,0,0q�6j   $	��Top=�6~   $��80��6�   6�   *  $Telerik.Windows.Controls.RadComboBox  5   
   -   5          Name$ Tryb_działania��5      $
��Left=�66   $��247,114,0,0q�6Q   $	��Top=�6f   $	��250��6~    �� SelectedIndex$ 0��6�   .@�5      .  (Telerik.Windows.Controls.RadComboBoxItem  $��Szybki��6&     5      $��Pokazowy��6&   5        5   
   -   5        $ Długość_słownika��5      $
��Left=�68   $��247,161,0,0q�6S   $	��Top=�6h   $	��250��6�   $ 0��6�   .@�5        $	��256��6&     5      $	��128��6&     5      $��64��6&     5      $��32��6&   65     5      $��16��6&   65   5        5   
   -   5        $ Dekompresuj��5   2   $��Dekompresuj��6   $
��Left=�6G   $��37��6b   $��319,365,0,0q�6n   $	��Top=�6�   $��80��6�   6�   �� 5   
   $��Wybierz tryb działania��6   $
��Left=�61   $��
13,114,0,0q�6L   $	��Top=�6`   $��31��6x   $	��190��6�   �� 5   
   $��Wybierz rozmiar słownika��6   $
��Left=�63   $��
13,161,0,0q�6N   $	��Top=�6b   $��31��6z   $	��190��6�   �� 5   
   $��Plik wejściowy��6   $
��Left=�6)   $��
13,209,0,0q�6D   $	��Top=�6X   $��32��6p   $	��190��6|   �� 5   
   $��Plik wyjściowy��6   $
��Left=�6)   $��
13,268,0,0q�6D   $	��Top=�6X   $��31��6p   $	��190��6|     5   
   -   5        $ Przeglądaj_zapisz��5   1   $��Przeglądaj��6   $
��Left=�6L   $��37��6g   $��263,253,0,0q�6s   $	��Top=�6�   $��85��6�   6�     5    
   -   5        $ Czysc��5    ,   $��Czyść��6   $
��Left=�6;   $��31��6V   $��417,308,0,0q�6b   $	��Top=�6w   $��80��6�   6�   �� 5!   
   $
��Left=�6   $	��290��6+   $��-97,-49,0,0q�68   $	��Top=�6M   $	��386��6e   $��LZSS.png��6q     5"   
   -   5        $ Kompresuj_Copy��5"   -   $��Wykres��6   $
��Left=�6E   $��37��6`   $��
417,23,0,0q�6l   $	��Top=�6�   $��80��6�   6�     5#   
   -	   5        $ 
Statystyka��5#   1   $��
Statystyka��6   $
��Left=�6E   $��37��6`   $��
319,23,0,0q�6l   $	��Top=�6�   $��80��6�   6�   5%      5&      