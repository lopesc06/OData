using System;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace oDATA.Controller
{
    public class BooksController : ODataController
    {
        #region Contructor
        private BookStoreContext _db;
        public BooksController(BookStoreContext context)
        {
            _db = context;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Press.Add(b.Press);
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region Resources

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }


        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_db.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }


        #endregion
    }
}
