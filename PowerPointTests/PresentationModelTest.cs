using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class PresentationModelTests
    {
        private PresentationModel.PresentationModel _presentationModel;
        private PrivateObject _privatePresentationModel;
        
        [TestInitialize()]
        public void Initialize()
        {
            _presentationModel = new PresentationModel.PresentationModel();
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }
    }
}