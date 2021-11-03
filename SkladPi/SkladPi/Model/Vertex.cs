using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SkladPi.Model
{
    public class Vertex
    {
        /// <summary>
        /// Folder name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Folder Path.
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// Products in the folder.
        /// </summary>
        public List<Product> Products { get; set; }
        /// <summary>
        /// Folders in the folder.
        /// </summary>
        public List<Vertex> Vertices { get; set; }
        /// <summary>
        /// Folder constructor.
        /// </summary>
        /// <param name="name"> Folder name. </param>
        public Vertex()
        {
            Vertices = new List<Vertex>(); 
            Products = new List<Product>();
        }
        /// <summary>
        /// Return folder for its path.
        /// </summary>
        /// <param name="fullPath"> Path. </param>
        /// <returns> Folder. </returns>
        public Vertex ReturnVertex(string fullPath)
        {
            return ReturnVertex(fullPath, this);
        }

        /// <summary>
        /// Recursively searches for a subfolder.
        /// </summary>
        /// <param name="fullPath"> Path. </param>
        /// <param name="vertex"> Folder. </param>
        /// <returns> Subfolder. </returns>
        private Vertex ReturnVertex(string fullPath, Vertex vertex)
        {
            if (vertex.FullPath != null && vertex.FullPath + @"\" + vertex.Name == fullPath
                || vertex.FullPath == null && vertex.Name == fullPath)
            {
                return vertex;
            }
            foreach (var item in vertex.Vertices)
            {
                Vertex vertex1 = ReturnVertex(fullPath, item);
                if (vertex1 != null)
                {
                    return vertex1;
                }
            }
            return null;
        }

        /// <summary>
        /// Rename folder.
        /// </summary>
        /// <param name="newName"> New folder name. </param>
        public void Rename(string newName)
        {
            Name = newName;

            foreach (var product in Products)
            {
                product.Path = (FullPath == null) ? Name : FullPath + '\\' + Name;
            }

            UpDatePath(this);
        }

        /// <summary>
        /// Rename all subfolder of folder.
        /// </summary>
        /// <param name="vertex"> Folder. </param>
        private void UpDatePath(Vertex vertex)
        {
            foreach (var item in vertex.Vertices)
            {
                item.FullPath = vertex.FullPath + @"\" + vertex.Name;

                foreach (var product in item.Products)
                {
                    product.Path = item.FullPath;
                }

                UpDatePath(item);
            }
        }

        /// <summary>
        /// Add subfolder to the folder.
        /// </summary>
        /// <param name="vertex"> Current vertex. </param>
        public void AddVertex(Vertex vertex)
        {          
            if (this.FullPath == null)
            {
                vertex.FullPath = this.Name;
            }
            else
            {
                vertex.FullPath = this.FullPath + @"\" + this.Name;
            }

            UpDatePath(vertex);
            this.Vertices.Add(vertex);
        }

        /// <summary>
        /// Get list of products at current vertex and all subvertex.
        /// </summary>
        /// <returns>List of products at current vertex and all subvertex. </returns>
        public List<Product> ReturnProducts()
        {
            List<Product> products = new List<Product>();
            ReturnProd(this, products);
            return products;
        }

        /// <summary>
        /// Get list of products.
        /// </summary>
        /// <param name="vertex"> Current vertex. </param>
        /// <param name="products"> List of products. </param>
        private void ReturnProd(Vertex vertex, List<Product> products)
        {
            foreach (var product in vertex.Products)
            {
                products.Add(product);
            }

            foreach (var child in vertex.Vertices)
            {
                ReturnProd(child, products);
            }
        }
        /// <summary>
        /// Return product according to its name.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>product</returns>
        public Product GetProduct(string name)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Name == name)
                {
                    return Products[i];
                }
            }
            
            return null;
        }
    }
}
