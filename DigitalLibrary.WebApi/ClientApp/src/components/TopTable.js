import React from "react";

const TopTable = props => {
  const { words } = props;
  return (
    <React.Fragment>
      <table className="table table-hover">
        <thead>
          <tr>
            <th className="col-1">#</th>
            <th className="col-9">Word</th>
            <th className="col-2">Count</th>
          </tr>
        </thead>
        <tbody>
          {words.map((item, index) => (
            <tr key={item.key}>
              <td>{index + 1}.</td>
              <td>
                {item.key
                  .toUpperCase()
                }
              </td>
              <td>{item.value}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </React.Fragment>
  );
};

export default TopTable;
