using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Lisa.Sample.WebApi
{
    public class PersonsController : ApiController
    {
        public IEnumerable<Person> Get()
        {
            return _db.Persons;
        }

        public IHttpActionResult Get(int id)
        {
            var person = _db.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        public IHttpActionResult Post([FromBody] Person person)
        {
            _db.Persons.Add(person);
            _db.SaveChanges();

            string url = String.Format("/persons/{0}", person.Id);
            return Created(url, person);
        }

        public IHttpActionResult Delete(int id)
        {
            var person = _db.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            _db.Persons.Remove(person);
            _db.SaveChanges();

            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] Person person)
        {
            var current = _db.Persons.Find(id);

            if (current == null)
            {
                return NotFound();
            }

            person.Id = id;
            _db.Entry<Person>(current).CurrentValues.SetValues(person);
            _db.SaveChanges();

            return Ok(current);
        }

        public IHttpActionResult Patch(int id, [FromBody] JToken json)
        {
            var person = _db.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            if (json["FirstName"] != null)
            {
                person.FirstName = json["FirstName"].ToString();
            }

            if (json["LastName"] != null)
            {
                person.LastName = json["LastName"].ToString();
            }

            _db.SaveChanges();

            return Ok(person);
        }

        private readonly SampleContext _db = new SampleContext();
    }
}