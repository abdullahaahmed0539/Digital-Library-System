// import { Counter } from "./components/Counter";
// import { FetchData } from "./components/FetchData";
import BookDetail from "./pages/Book";
import Home from "./pages/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "Books/:id",
    element: <BookDetail />,
  },
  // {
  //   path: '/fetch-data',
  //   element: <FetchData />
  // }
];

export default AppRoutes;
