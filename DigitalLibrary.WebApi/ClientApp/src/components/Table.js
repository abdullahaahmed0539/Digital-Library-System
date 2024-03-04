import React from "react";

const Table = props => {
  const { books, showModal } = props;
  return (
    <React.Fragment>
      <table className="table table-hover mt-4">
        <thead>
          <tr>
            <th className="col-1">#</th>
            <th className="col-10">Book Name</th>
            <th className="col-1">Action</th>
          </tr>
        </thead>
        <tbody>
          {books.map((item, index) => (
            <tr key={item.id}>
              <td>{index + 1}.</td>
              <td>
                <a href={`Books/${item.id}`} style={{ color: "black" }}>
                  {item.name
                    .toLowerCase()
                    .split(" ")
                    .map(word => word.charAt(0).toUpperCase() + word.slice(1))
                    .join(" ")}
                </a>
              </td>
              <td style={{ textAlign: ":center" }}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="16"
                  height="16"
                  fill="currentColor"
                  className="bi bi-trash"
                  viewBox="0 0 16 16"
                  style={{ color: "red", cursor: "pointer" }}
                  onClick={() => showModal(item.id,item.name)}
                >
                  <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" />
                  <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z" />
                </svg>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </React.Fragment>
  );
};

export default Table;
