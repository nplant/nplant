using System;
using System.Collections.Generic;

namespace NPlant.Core
{
    public class TypeMetaModelSet
    {
        private readonly IDictionary<Type, TypeMetaModel> _dictionary = new Dictionary<Type, TypeMetaModel>();

        public TypeMetaModel this[Type type]
        {
            get
            {
                TypeMetaModel model;

                if (!_dictionary.TryGetValue(type, out model))
                {
                    model = new TypeMetaModel(type);
                    _dictionary.Add(type, model);
                }

                return model;
            }
            set { _dictionary[type] = value; }
        }
    }
}