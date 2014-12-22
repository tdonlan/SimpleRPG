using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryGenTestMay1
{

    public class Paragraph
    {
        public List<Sentence> sentenceList = new List<Sentence>();

        public Paragraph()
        { }
    }

    public class Section
    {
        public List<Paragraph> paraList = new List<Paragraph>();

        public Section()
        { }
    }


    public class Story
    {
        public List<Section> sectionList = new List<Section>();

        public Story()
        {

        }
    }
}
