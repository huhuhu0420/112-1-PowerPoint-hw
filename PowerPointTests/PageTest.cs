using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace PowerPoint.Tests
{
    [TestClass]
    public class PagesTests
    {
        private Pages _pages;
        private Mock<Shape> _mockShape;

        [TestInitialize]
        public void Setup()
        {
            _pages = new Pages();
            _mockShape = new Mock<Shape>();
        }

        // test
        [TestMethod]
        public void AddPageTest()
        {
            var initialCount = _pages.GetPageCount();

            _pages.AddPage();

            Assert.AreEqual(initialCount + 1, _pages.GetPageCount());
        }

        // test
        [TestMethod]
        public void AddPageByIndexTest()
        {
            _pages._pagesChanged += (isAdd, index) => { };
            var page = new System.ComponentModel.BindingList<Shape> { _mockShape.Object };

            _pages.AddPageByIndex(0, page);

            Assert.AreEqual(page, _pages.GetPage(0));
        }

        // test
        [TestMethod]
        public void RemovePageByIndexTest()
        {
            _pages._pagesChanged += (isAdd, index) => { };
            _pages.AddPage();
            _pages.AddPage();
            var initialCount = _pages.GetPageCount();
            _pages.RemovePageByIndex(0);

            Assert.AreEqual(initialCount - 1, _pages.GetPageCount());
        }

        // test
        [TestMethod]
        public void RemovePageByIndexWhenOnlyOnePageTest()
        {
            _pages.AddPage();

            _pages.RemovePageByIndex(0);

            Assert.AreEqual(1, _pages.GetPageCount());
        }

        // test
        [TestMethod]
        public void ClearTest()
        {
            _pages.AddPage();
            _pages.AddPage();

            _pages.Clear();

            Assert.AreEqual(1, _pages.GetPageCount());
        }

        // test
        [TestMethod]
        public void GetPageTest()
        {
            var page = new System.ComponentModel.BindingList<Shape> { _mockShape.Object };
            _pages.AddPageByIndex(0, page);

            Assert.AreEqual(page, _pages.GetPage(0));
        }

        // test
        [TestMethod]
        public void GetEncodeTest()
        {
            _mockShape.Setup(s => s.GetEncode()).Returns("shape1");
            var page = new System.ComponentModel.BindingList<Shape> { _mockShape.Object };
            _pages.AddPageByIndex(0, page);
            _pages.AddPageByIndex(0, page);

            var expectedEncoding = "shape1\npage\nshape1\n";

            Assert.AreEqual(expectedEncoding, _pages.GetEncode());
        }
    }
}