namespace IGK.ICore.Xml.XSD
{
    public  interface ICoreXSDAttribute : ICoreXSDType
    {
         string Default { get; }
         string Fixed { get;  }
         string Form { get;  }
         string Id { get; }
         string Ref { get;  }
         string Type { get;  }
         string Use { get;  }
    }
}