﻿using System;

namespace WebService.Models
{
    public class ReturnTypeConstants
    {
        private string ReturnType { get; set; }

        enum TypeConstants
        {
            posts,
            questions,
            notes
        }

        /// <summary>
        /// Choose a string type matching any name available in the TypeConstants enum. The return types should never change.
        /// </summary>
        public ReturnTypeConstants(string type)
        {
            if(!Enum.IsDefined(typeof(TypeConstants), type))
            {
                throw new ArgumentException("Not a correct return type. See the available types in TypeConstants.");
            }

            this.ReturnType = type;
        }

        public override string ToString()
        {
            var value = "";
            switch (ReturnType)
            {
                case "posts":
                    value = TypeConstants.posts.ToString();
                    break;
                case "questions":
                    value = TypeConstants.questions.ToString();
                    break;
                case "notes":
                    value = TypeConstants.notes.ToString();
                    break;
            }
            return value;
        }
    }
}
