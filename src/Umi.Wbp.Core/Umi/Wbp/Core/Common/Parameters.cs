using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Umi.Wbp.Core.Common
{
    /// <summary>
    /// This is a generic parameters base class used for Dialog Parameters and Navigation Parameters.
    /// </summary>
    public class Parameters : IParameters, IEnumerable<KeyValuePair<string, object>>
    {
        private readonly List<KeyValuePair<string, object>> _entries = new List<KeyValuePair<string, object>>();

        public Parameters()
        {
        }

        /// <summary>
        /// Searches Parameter collection and returns value if Collection contains key.
        /// Otherwise returns null.
        /// </summary>
        /// <param name="key">The key for the value to be returned.</param>
        /// <returns>The value of the parameter referenced by the key; otherwise <c>null</c>.</returns>
        public object this[string key]
        {
            get
            {
                foreach (var entry in _entries)
                {
                    if (string.Compare(entry.Key, key, StringComparison.Ordinal) == 0)
                    {
                        return entry.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// The count, or number, of parameters in collection.
        /// </summary>
        public int Count => _entries.Count;

        /// <summary>
        /// Returns an IEnumerable of the Keys in the collection.
        /// </summary>
        public IEnumerable<string> Keys =>
            _entries.Select(x => x.Key);

        /// <summary>
        /// Adds the key and value to the parameters collection.
        /// </summary>
        /// <param name="key">The key to reference this value in the parameters collection.</param>
        /// <param name="value">The value of the parameter to store.</param>
        public void Add(string key, object value) =>
            _entries.Add(new KeyValuePair<string, object>(key, value));

        /// <summary>
        /// Checks collection for presence of key.
        /// </summary>
        /// <param name="key">The key to check in the collection.</param>
        /// <returns><c>true</c> if key exists; else returns <c>false</c>.</returns>
        public bool ContainsKey(string key) =>
            _entries.ContainsKey(key);

        /// <summary>
        /// Gets an enumerator for the KeyValuePairs in parameter collection.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            _entries.GetEnumerator();

        /// <summary>
        /// Returns the value of the member referenced by key.
        /// </summary>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        /// <param name="key">The key for the value to be returned.</param>
        /// <returns>Returns a matching parameter of <typeparamref name="T"/> if one exists in the Collection.</returns>
        public T GetValue<T>(string key) =>
            _entries.GetValue<T>(key);

        /// <summary>
        /// Returns an IEnumerable of all parameters.
        /// </summary>
        /// <typeparam name="T">The type for the values to be returned.</typeparam>
        /// <param name="key">The key for the values to be returned.</param>
        ///<returns>Returns a IEnumerable of all the instances of type <typeparamref name="T"/>.</returns>
        public IEnumerable<T> GetValues<T>(string key) =>
            _entries.GetValues<T>(key);

        /// <summary>
        /// Checks to see if the parameter collection contains the value.
        /// </summary>
        /// <typeparam name="T">The type for the values to be returned.</typeparam>
        /// <param name="key">The key for the value to be returned.</param>
        /// <param name="value">Value of the returned parameter if it exists.</param>
        public bool TryGetValue<T>(string key, out T value) =>
            _entries.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Converts parameter collection to a parameter string.
        /// </summary>
        /// <returns>A string representation of the parameters.</returns>
        public override string ToString()
        {
            var queryBuilder = new StringBuilder();

            if (_entries.Count > 0)
            {
                queryBuilder.Append('?');
                var first = true;

                foreach (var kvp in _entries)
                {
                    if (!first)
                    {
                        queryBuilder.Append('&');
                    }
                    else
                    {
                        first = false;
                    }

                    queryBuilder.Append(Uri.EscapeDataString(kvp.Key));
                    queryBuilder.Append('=');
                    queryBuilder.Append(Uri.EscapeDataString(kvp.Value != null ? kvp.Value.ToString() : ""));
                }
            }

            return queryBuilder.ToString();
        }

        /// <summary>
        /// Adds a collection of parameters to the local parameter list.
        /// </summary>
        /// <param name="parameters">An IEnumerable of KeyValuePairs to add to the current parameter list.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void FromParameters(IEnumerable<KeyValuePair<string, object>> parameters) =>
            _entries.AddRange(parameters);
        
        protected bool Equals(Parameters other){
            return _entries.All(other.Contains);
        }

        public override bool Equals(object obj){
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Parameters)obj);
        }

        public override int GetHashCode(){
            return _entries?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Parameters left, Parameters right){
            return Equals(left, right);
        }

        public static bool operator !=(Parameters left, Parameters right){
            return !Equals(left, right);
        }
    }
}
