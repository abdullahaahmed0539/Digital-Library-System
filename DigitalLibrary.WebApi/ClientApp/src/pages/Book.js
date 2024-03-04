import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import TopTable from "../components/TopTable";
import axios from "axios";

const getBook = async id => {
  const response = await axios.get(`https://localhost:7271/api/Books/${id}`);
  return response;
};

const getMatchedWordsByPrefix = async (id, prefix) => {
  const response = await axios.get(
    `https://localhost:7271/api/Books/${id}/Search/`,
    {
      params: {
        prefix,
      },
    }
  );
  return response;
};

const matchByPrefix = async (id, prefix, setMatchedWord) => {
  if (prefix.length >= 3) {
    const response = await getMatchedWordsByPrefix(id, prefix);
    return response.data.matchedWords;
  }
  return [];
};

const BookDetail = () => {
  const { id } = useParams();
  const [book, setBook] = useState(undefined);
  const [matchedWords, setMatchedWords] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    getBook(id)
      .then(response => {
        setBook(response.data);
      })
      .catch(error => setError(error));
  }, []);

  if (book === undefined) {
    return <div>Loading...</div>;
  } else if (book !== undefined && !error) {
    return (
      <div className="mt-4 mb-4">
        <h1>
          <u>Summary - {book.bookName}</u>
        </h1>
        <br />
        <div className="mt-4">
          <h4>Here are the top 10 words in this book:</h4>
          <TopTable words={book.topTenWords}></TopTable>
        </div>
        <br />
        <div className="mt-4">
          <h4>Search words to check the number of occurence</h4>
          <form>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                id="exampleInputEmail1"
                aria-describedby="emailHelp"
                onChange={async e => {
                  const newMatchedWords = await matchByPrefix(
                    id,
                    e.target.value
                  );
                  setMatchedWords(newMatchedWords);
                }}
              />
            </div>
          </form>
        </div>

        {matchedWords.length > 0 ? (
          <TopTable words={matchedWords}></TopTable>
        ) : (
          <p>Enter minimum 3 characters.</p>
        )}
      </div>
    );
  } else {
    return <p>There was an error while loading book details</p>;
  }
};

export default BookDetail;
