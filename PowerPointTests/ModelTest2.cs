using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests2
    {
        private Model _model;
        PrivateObject _privateModel;
        private Mock<Context> _mockContext;

        // test
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model();
            _mockContext = new Mock<Context>(_model);
            _privateModel = new PrivateObject(_model);
            _model.SetContext(_mockContext.Object);
        }

        // test
        [TestMethod]

        public void RemoveShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            _model.RemoveShape();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test
        [TestMethod]

        public void RemoveShapeByIndexTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 1);
            _model.RemoveShapeByIndex(0);

            Assert.AreEqual(0, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void InsertShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void SetContestTest()
        {
            _model.SetContext(_mockContext.Object);
            var context = _privateModel.GetField(Constant.CONTEXT);
            Assert.AreEqual(_mockContext.Object, context);
        }
    }
}