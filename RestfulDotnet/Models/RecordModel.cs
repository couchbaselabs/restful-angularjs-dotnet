using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;

namespace RestfulDotnet.Models
{
    public class RecordModel
    {
        readonly IBucket _bucket;

        public RecordModel()
        {
            _bucket = ClusterHelper.GetBucket(ConfigurationManager.AppSettings["CouchbaseBucket"]);
        }

        public async Task<dynamic> GetByDocumentId(Guid documentId)
        {
            var n1ql = "SELECT firstname, lastname, email " +
                       "FROM `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + "` AS users " +
                       "WHERE META(users).id = $1";
            var query = QueryRequest.Create(n1ql);
            query.AddPositionalParameter(documentId);

            var r = await _bucket.QueryAsync<dynamic>(query);
            return r.Rows;
        }

        public async Task<dynamic> GetAll()
        {
            var n1ql = "SELECT META(users).id, firstname, lastname, email " +
                        "FROM `" + ConfigurationManager.AppSettings["CouchbaseBucket"] + "` AS users";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);

            var r = await _bucket.QueryAsync<dynamic>(query);
            return r.Rows;
        }

        public async Task<IDocumentResult<dynamic>> Save(Person data)
        {
            var document = new Document<dynamic>
            {
                Id = data.Document_Id?.ToString() ?? Guid.NewGuid().ToString(),
                Content = new
                {
                    firstname = data.FirstName,
                    lastname = data.LastName,
                    email = data.Email
                }
            };

            return await _bucket.UpsertAsync(document);
        }

        public async Task<IOperationResult> Delete(Guid documentId)
        {
            return await _bucket.RemoveAsync(documentId.ToString());
        }
    }
}