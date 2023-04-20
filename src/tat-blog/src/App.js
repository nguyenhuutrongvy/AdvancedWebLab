import './App.css';
// import Navbar from './Components/Navbar';
// import Sidebar from './Components/Sidebar';
// import Footer from './Components/Footer';
import Layout from './Pages/Layout';
import Index from './Pages/Index'
import About from './Pages/About'
import Contact from './Pages/Contact'
import Rss from './Pages/Rss'
import { Route, Routes, BrowserRouter as Router } from 'react-router-dom';
import AdminLayout from "./Pages/Admin/Layout";
import * as AdminIndex from "./Pages/Admin/Index";
import Authors from "./Pages/Admin/Authors";
import Categories from "./Pages/Admin/Categories";
import Comments from "./Pages/Admin/Comments";
import Tags from "./Pages/Admin/Tags";
import Posts from "./Pages/Admin/Post/Posts";
import NotFound from "./Pages/NotFound";
import BadRequest from './Pages/BadRequest';
import Edit from "./Pages/Admin/Post/Edit";

function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<Layout />}>
          <Route path='/' element={<Index />} />
          <Route path='blog' element={<Index />} />
          <Route path='blog/Contact' element={<Contact />} />
          <Route path='blog/About' element={<About />} />
          <Route path='blog/RSS' element={<Rss />} />
        </Route>
        <Route path='/admin' element={<AdminLayout />}>
          <Route path='/admin' element={<AdminIndex.default />}></Route>
          <Route path='/admin/authors' element={<Authors />}></Route>
          <Route path='/admin/categories' element={<Categories />}></Route>
          <Route path='/admin/comments' element={<Comments />}></Route>
          <Route path='/admin/posts' element={<Posts />}></Route>
          <Route path='/admin/posts/edit' element={<Edit />} />
          <Route path='/admin/posts/edit/:id' element={<Edit />} />
          <Route path='/admin/tags' element={<Tags />}></Route>
        </Route>
        <Route path='/400' element={<BadRequest />}></Route>
        <Route path='*' element={<NotFound />}></Route>
      </Routes>
    </Router>
  );
}

export default App;