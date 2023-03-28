#Tweet Analyzer

Project objectives

The project is a visualization of twitter data across the USA. It
display a map that depicts how the people in different states feels.

This project is done by:
1. Collecting public Twitter posts (Tweets) that have been tagged with geographic locations and
filtering for those that contain the «cali» query term.

2. Assigning a sentiment (positive or negative) to each Tweet, based on all of the words it
contains.

3. Aggregating Tweets by the state with the closest geographic center, and finally

4. Coloring each state according to the aggregate sentiment of its Tweets. Yellow means
positive sentiment; blue means negative.

The Data directory contains all the data files needed for the project, and it's necessary to run
the project. Each file in this folder (cali_tweets2014.txt, family_tweets2014.txt,
football_tweets2014.txt, movie_tweets2014.txt, ...) contains collection of tweets on a related
topic: California, family, football, movies, ...

Every tweet is represented as following: the latitude of the tweet's location, the longitude of
the tweet's location, date and time when the tweet was posted, the text of the tweet.

Some words of the tweet are associated with positive or negative sentiment, but most are not.
The sentiment of some individual words can be found in the Data/sentiments.csv text file.

Json file Data/states.json contains data on geographical location of each U.S. state. Each state is
keyed by its two-letter postal code (WA, FL, HI) and the shape. The shape of a state is
represented as a list of polygons. Some states (e.g. Hawaii) consist of multiple polygons, but
most states (e.g. Colorado) consist of only one polygon (represented as a length-one list of
polygons).


Phase 1: The Feelings in Tweets

Splitted the text of a tweet into words, and calculate the amount of
positive or negative feeling in a tweet.

Problem 1. Accessed the word of a tweet. Extracted words from tweet.

Problem 2. Analyze tweet sentiment: take a tweet and return a single number averaging the
weights of sentiment-carrying words in the tweet, or None if none of the words in the tweet
carry a sentiment weight.

Phase 2: The Mood of the Nation
Determined the state that a tweet is coming from, group tweets by state,
and calculate the average positive or negative feeling in all the tweets associated with a state.

Problem 3. Implement function which returns the two-letter postal code of the state that is
closest to the location of a tweet.

Problem 4. Group tweets by state. Take a list of all tweets and return a dictionary. The keys of
the returned dictionary are state postal codes, and the values are lists of tweets that appear
closer to that state's center than any other.

Problem 5. Implement calculate_average_sentiments. This function takes the dictionary
returned by group_tweets_by_state and also returns a dictionary. The keys of the returned
dictionary are the state names (two-letter postal codes), and the values are average sentiment
values for all the tweets in that state.
If a state has no tweets with sentiment values, leave it out of the dictionary entirely. Do not
include states with no tweets, or with tweets that have no sentiment, with a zero sentiment
value. Zero represents neutral sentiment, not unknown sentiment. States with unknown
sentiment will appear gray, while states with neutral sentiment will appear white.
Problem 6. You should now be able to draw maps that are colored by sentiment corresponding
to tweets that contain a given term. The correct map for «cali» appears at the top of this
document.
