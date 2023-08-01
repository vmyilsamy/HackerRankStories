namespace BestNStories.Response
{
    internal class EmptyStory : Story
    {
        public EmptyStory(int storyId)
        {
            StoryId = storyId;
            Title = "Story not found";
        }

        public int StoryId { get; private set; }

    }
}