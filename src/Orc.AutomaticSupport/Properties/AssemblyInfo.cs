// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Markup;

// All other assembly info is defined in SharedAssembly.cs

[assembly: AssemblyTitle("Orc.AutomaticSupport")]
[assembly: AssemblyProduct("Orc.AutomaticSupport")]
[assembly: AssemblyDescription("Orc.AutomaticSupport library")]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: XmlnsPrefix("http://schemas.wildgums.com/orc/automaticsupport", "orcautomaticsupport")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Behaviors")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Controls")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Converters")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Fonts")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Markup")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Views")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/automaticsupport", "Orc.AutomaticSupport.Windows")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
    )]
