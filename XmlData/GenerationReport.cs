﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Generation {
    
    private GenerationDay[] dayField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Day", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public GenerationDay[] Day {
        get {
            return this.dayField;
        }
        set {
            this.dayField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationDay {
    
    private string dateField;
    
    private string energyField;
    
    private string priceField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Date {
        get {
            return this.dateField;
        }
        set {
            this.dateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Energy {
        get {
            return this.energyField;
        }
        set {
            this.energyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Price {
        get {
            return this.priceField;
        }
        set {
            this.priceField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class GenerationReport {
    
    private object[] itemsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Coal", typeof(GenerationReportCoal), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("Gas", typeof(GenerationReportGas), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("Generation", typeof(Generation))]
    [System.Xml.Serialization.XmlElementAttribute("Wind", typeof(GenerationReportWind), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportCoal {
    
    private GenerationReportCoalCoalGenerator[] coalGeneratorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CoalGenerator", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public GenerationReportCoalCoalGenerator[] CoalGenerator {
        get {
            return this.coalGeneratorField;
        }
        set {
            this.coalGeneratorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportCoalCoalGenerator {
    
    private string nameField;
    
    private string totalHeatInputField;
    
    private string actualNetGenerationField;
    
    private string emissionsRatingField;
    
    private GenerationDay[][] generationField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TotalHeatInput {
        get {
            return this.totalHeatInputField;
        }
        set {
            this.totalHeatInputField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActualNetGeneration {
        get {
            return this.actualNetGenerationField;
        }
        set {
            this.actualNetGenerationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string EmissionsRating {
        get {
            return this.emissionsRatingField;
        }
        set {
            this.emissionsRatingField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Day", typeof(GenerationDay), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public GenerationDay[][] Generation {
        get {
            return this.generationField;
        }
        set {
            this.generationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportGas {
    
    private GenerationReportGasGasGenerator[] gasGeneratorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GasGenerator", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public GenerationReportGasGasGenerator[] GasGenerator {
        get {
            return this.gasGeneratorField;
        }
        set {
            this.gasGeneratorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportGasGasGenerator {
    
    private string nameField;
    
    private string emissionsRatingField;
    
    private GenerationDay[][] generationField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string EmissionsRating {
        get {
            return this.emissionsRatingField;
        }
        set {
            this.emissionsRatingField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Day", typeof(GenerationDay), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public GenerationDay[][] Generation {
        get {
            return this.generationField;
        }
        set {
            this.generationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportWind {
    
    private GenerationReportWindWindGenerator[] windGeneratorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("WindGenerator", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public GenerationReportWindWindGenerator[] WindGenerator {
        get {
            return this.windGeneratorField;
        }
        set {
            this.windGeneratorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GenerationReportWindWindGenerator {
    
    private string nameField;
    
    private string locationField;
    
    private GenerationDay[][] generationField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Location {
        get {
            return this.locationField;
        }
        set {
            this.locationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Day", typeof(GenerationDay), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public GenerationDay[][] Generation {
        get {
            return this.generationField;
        }
        set {
            this.generationField = value;
        }
    }
}
