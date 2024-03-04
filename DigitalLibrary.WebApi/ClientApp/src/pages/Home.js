import { useEffect, useState } from "react";
import axios from "axios";
import { Button, Modal } from "react-bootstrap";
import { useRef } from "react";

import Table from "../components/Table";

const getBooks = async () => {
  const response = await axios.get(`https://localhost:7271/api/Books/`);
  return response;
};

const addBook = async (bookName, bookFile) => {
  const response = await axios.post(
    `https://localhost:7271/api/Books/`,
    {
      bookName,
      bookFile,
    },
    {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }
  );
  return response;
};

const deleteBook = async toBeDeletedId => {
  const response = await axios.delete(
    `https://localhost:7271/api/Books/${toBeDeletedId}`
  );
  return response;
};

const Home = () => {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [nameAdded, setNameAdded] = useState("");
  const [fileAdded, setFileAdded] = useState(null);
  const [toBeDeletedId, setToBeDeletedId] = useState("");
  const [toBeDeletedName, setToBeDeletedName] = useState("");
  const [bookAdded, setBookAdded] = useState(false);
  const [bookDeleted, setBookDeleted] = useState(false);

  const handleCloseAddModal = () => setShowAddModal(false);
  const handleShowAddModal = () => setShowAddModal(true);
  const handleCloseDeleteModal = () => setShowDeleteModal(false);
  const handleShowDeleteModal = (id, name) => {
    setToBeDeletedId(id);
    setToBeDeletedName(name);
    setShowDeleteModal(true);
  };

  const onAddSubmit = async (bookName, inputFile) => {
    setBookAdded(false);
    addBook(bookName, inputFile)
      .then(res => {
        alert("Book has been added.");
        setBookAdded(true);
      })
      .catch(err => alert(`There was an error uploading the book. ${err}`));
  };

  const onDeleteSubmit = async toBeDeletedId => {
    setBookDeleted(false);
    deleteBook(toBeDeletedId)
      .then(res => {
        alert("The book has been deleted successfully.");
        setBookDeleted(true);
      })
      .catch(err => alert(`There was an error deleting the book. ${err}`));
  };

  const renderAddModal = () => {
    return (
      <Modal show={showAddModal} onHide={handleCloseAddModal}>
        <Modal.Header closeButton>
          <Modal.Title>Enter book details</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="mb-3">
              <div className="form-group">
                <label htmlFor="exampleInputEmail1" className="form-label">
                  Name
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="exampleInputEmail1"
                  aria-describedby="emailHelp"
                  onChange={e => {
                    setNameAdded(e.target.value);
                  }}
                />
              </div>
              <div className="form-group">
                <br />
                <label htmlFor="exampleFormControlFile1">
                  Choose a .txt file
                </label>
                <br />
                <input
                  type="file"
                  className="form-control-file mt-2"
                  id="exampleFormControlFile1"
                  accept=".txt"
                  onChange={e => {
                    setFileAdded(e.target.files[0]);
                  }}
                />
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseAddModal}>
            Close
          </Button>
          {nameAdded.length > 0 && fileAdded != null ? (
            <Button
              variant="primary"
              onClick={() => {
                onAddSubmit(nameAdded, fileAdded);
                handleCloseAddModal();
              }}
              disabled={false}
            >
              Save Changes
            </Button>
          ) : (
            <Button variant="primary" disabled={true}>
              Save Changes
            </Button>
          )}
        </Modal.Footer>
      </Modal>
    );
  };

  const renderDeleteModal = name => {
    return (
      <Modal show={showDeleteModal} onHide={handleCloseDeleteModal}>
        <Modal.Header closeButton>
          <Modal.Title>Delete book</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure you want to delete {`\"${toBeDeletedName}\"`}?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseDeleteModal}>
            Close
          </Button>

          <Button
            variant="danger"
            onClick={() => {
              onDeleteSubmit(toBeDeletedId);
              handleCloseDeleteModal();
            }}
            disabled={false}
          >
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    );
  };

  useEffect(() => {
    setLoading(true);
    getBooks()
      .then(response => setBooks(response.data.books))
      .catch(error => setError(error));
    setLoading(false);
  }, [bookAdded, bookDeleted]);

  if (loading) {
    return <p>Loading...</p>;
  } else if (!loading && !error) {
    return (
      <div className="container">
        {books.length > 0 ? (
          <div className="row">
            <div>
              <Button
                variant="primary"
                className="float-end mt-4"
                onClick={handleShowAddModal}
              >
                Add Book
              </Button>

              {renderAddModal()}
              {renderDeleteModal()}
            </div>
            <Table books={books} showModal={handleShowDeleteModal}></Table>
          </div>
        ) : (
          <div className="container">
            <div className="row">
              <div>
                <p className="mt-4">There are no books to show.</p>
                <Button
                  variant="primary"
                  className="mt-1"
                  onClick={handleShowAddModal}
                >
                  Add Book
                </Button>
                {renderAddModal()}
              </div>
            </div>
          </div>
        )}
      </div>
    );
  } else if (error) {
    return <p>There was an error while loading books. Please try later.</p>;
  }
};

export default Home;
