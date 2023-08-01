using System.Collections.Generic;

namespace BestNStories.Response
{
    public class StoryIdCollection : List<int>
    {
        public StoryIdCollection()
        {
            StoryIds = new int[]{};
        }

        public int[] StoryIds { get; set; }
    }
}