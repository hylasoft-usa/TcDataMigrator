   M S B A M L   `   `   `  ���� E  ATCMigrator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TCMigrator.Standalone.DB2CSVTCMigrator.Standalone.DB2CSV  ,    &TCMigrator.Standalone.DB2CSV.Csv2Tcxml   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:TCMigrator.Standalone.DB2CSV       N� Title$  	Csv2Tcxml��.���  � RowDefinitions �� �� $��5* ��� .+�� $��1�� � ColumnDefinitions �� �� $��3* ��� �� $��3* � �� �� �� �� �� �� �� .+��� $��Stretch=�$��5q�$
��Wrap=�$��	CSV Path:��$��Center=� �� TextAlignment$ Right=��� $��Stretch=�$��1��$��5q�$
��Wrap=�$��	Encoding:��$��Center=�$ Right=��� $��Stretch=�$��2��$��5q�$
��Wrap=�$��CSV Separator:��$��Center=�$ Right=��� $��Stretch=�$��3��$��5q�$
��Wrap=�$��CSV Quotation:��$��Center=�$ Right=��� $��Stretch=�$��4��$��5q�$
��Wrap=�$��CSV Escape:��$��Center=�$ Right=��� $��Stretch=�$��5��$��5q�$
��Wrap=�$��Source Site:��$��Center=�$ Right=��� $��Stretch=�$��2��$��3��$��5q�$
��Wrap=�$��TCXML VLA Separator:��$��Center=�$ Right=��� $��Stretch=�$��2��$��5q�$
��Wrap=�$��Island Size:��$��Center=�$ Right=��� $��Stretch=�$��2��$��1��$��5q�$
��Wrap=�$��BOM View Type:��$��Center=�$ Right=��� $��Stretch=�$��2��$��2��$��5q�$
��Wrap=�$��BOM View Revision Type:��$��Center=�$ Right=�� $��2��$��Stretch=�$��6��$��Stretch=� �� �� �� .+��� -    ��Name$ SaveGSID��$��Save GSID Map?��$��Center=�$��5q�$��Center=�   RoundedTextbox#��[  �� -   $ UseBVRPercise��$��Use BVR Percise?��$��1��$��Center=�$��5q�$��Center=�#��[  �� -   $ ValidateLovs��$��Validate LOVs?��$��2��$��Center=�$��5q�$��Center=�#��[  � $��2��$��4��$��2��$��3�� �� ��  �� �� �� �� .+��� -   $ 
SkipExists��$��Skip Existing?��$
��Left=�$��5q�$��Center=�#���   �� -   $ GroupBy��$��Group By Type?��$��1��$
��Left=�$��5q�#��[  $��Center=��� -   $ UseLocal��$��Use Local Time?��$��2��$
��Left=�$��5q�$��Center=�#��[  �� -    ��Name$ SkipExistingType��$��1��$��Stretch=�$��22��$��5q� �� TextWrapping$
 Wrap=�$�� ��$��Center=��� -   $ GroupByType��$��1��$��1��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -	   $ LocalTimeOffset��$��3��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -
   $ CsvPath��$��1��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ CsvSep��$��1��$��2��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ CsvQuote��$��1��$��3��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ 	CsvEscape��$��1��$��4��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ 
SourceSite��$��1��$��5��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ 
IslandSize��$��3��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ BomViewType��$��3��$��1��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ BomViewRevisionType��$��3��$��2��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -   $ TCXMLSep��$��3��$��3��$��Stretch=�$��22��$��5q�$
 Wrap=�$�� ��$��Center=��� -    ��Name$ FileEncoding��$��1��$��Stretch=�$��5q�$��1��$��Center=�� $��2�� �� �� �� �� .+��� -   $��3��$��Proceed��$��Stretch=�$��5q�$��2��$��Center=�  EngGreenBtn#��[ �� -   $
��Back��$��Stretch=�$��5q�$��2��$��Center=�  	EngRedBtn#��[ 