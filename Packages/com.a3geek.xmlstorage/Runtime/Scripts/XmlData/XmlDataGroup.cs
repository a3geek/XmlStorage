using System;
using System.Collections.Generic;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataGroup : XmlDataGroupModel
    {
        public readonly string GroupName;
        public readonly XmlDataElement[] Elements;
        
        
        
        
        public XmlDataGroup(in DataGroup group)
        {
            this.GroupName = group.GroupName;
            this.Elements = group
                .GetData()
                .GetElements()
                .Select(e => new XmlDataElement(e))
                .ToArray();
        }

        public XmlDataGroup(in XmlDataGroupModel model)
        {
            this.GroupName = model.GroupName;

            this.Elements = new XmlDataElement[model.Elements.Length];
            for(var i = 0; i < model.Elements.Length; i++)
            {
                this.Elements[i] = new XmlDataElement(model.Elements[i]);
            }
        }

        public void GetModel(out XmlDataGroupModel model)
        {
            var em = new XmlDataElementModel[this.Elements.Length];
            
            
            model = new XmlDataGroupModel(
                this.GroupName
            );
        }

        public IEnumerator<XmlDataElement> GetEnumerator()
        {
            foreach (var element in this.Elements)
            {
                yield return element;
            }
        }
    }
}
