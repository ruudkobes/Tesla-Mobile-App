﻿using Exrin.Abstraction;
using Exrin.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesla.Model
{
    public class IsPinValidModelExecute : IModelExecute<bool>
    {

        private readonly string _pin = "";

        public IsPinValidModelExecute(string pin)
        {
            _pin = pin;
        }

        public IOperation<bool> Operation
        {
            get
            {
                return new Operation<bool>()
                {
                    Function = () =>
                    {
                        if (_pin == null)
                            return Task.FromResult(false);

                        var result = 0;
                        if (!Int32.TryParse(_pin, out result))
                            return Task.FromResult(false);

                        if (_pin.Length == 4)
                            return Task.FromResult(true);
                        else
                            return Task.FromResult(false);
                    }
                };
            }
        }

        public int TimeoutMilliseconds { get; set; } = 10000;
    }
}
