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
public partial class ReferenceData {
    
    private ReferenceDataFactors[] itemsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Factors", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ReferenceDataFactors[] Items {
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
public partial class ReferenceDataFactors {
    
    private ReferenceDataFactorsValueFactor[] valueFactorField;
    
    private ReferenceDataFactorsEmissionsFactor[] emissionsFactorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ValueFactor", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ReferenceDataFactorsValueFactor[] ValueFactor {
        get {
            return this.valueFactorField;
        }
        set {
            this.valueFactorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("EmissionsFactor", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ReferenceDataFactorsEmissionsFactor[] EmissionsFactor {
        get {
            return this.emissionsFactorField;
        }
        set {
            this.emissionsFactorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ReferenceDataFactorsValueFactor {
    
    private string highField;
    
    private string mediumField;
    
    private string lowField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string High {
        get {
            return this.highField;
        }
        set {
            this.highField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Medium {
        get {
            return this.mediumField;
        }
        set {
            this.mediumField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Low {
        get {
            return this.lowField;
        }
        set {
            this.lowField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ReferenceDataFactorsEmissionsFactor {
    
    private string highField;
    
    private string mediumField;
    
    private string lowField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string High {
        get {
            return this.highField;
        }
        set {
            this.highField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Medium {
        get {
            return this.mediumField;
        }
        set {
            this.mediumField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Low {
        get {
            return this.lowField;
        }
        set {
            this.lowField = value;
        }
    }
}