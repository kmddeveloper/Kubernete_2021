using Kubernetes.DataReaderMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernetes.Repository
{
    public interface IMaps
    {
        Dictionary<string, MapRule> UserMap { get;  }

        Dictionary<string, MapRule> SessionMap { get; }

        Dictionary<string, MapRule> ProductMap { get; }

        Dictionary<string, MapRule> ProductCategory { get; }

        Dictionary<string, MapRule> ProductStatus { get; }

        Dictionary<string, MapRule> ItemInCartMap { get; }
        Dictionary<string, MapRule> ProductSpec { get; }
        
    }
}
