﻿using _0_FrameWork.Domain;

namespace ShopManagment.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string Title { get; private set; }
        public string KeyWord { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public ProductCategory(string name, string description, string picture, string pictureAlt, string title, string keyWord, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            Title = title;
            KeyWord = keyWord;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string title, string keyWord, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            Title = title;
            KeyWord = keyWord;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
