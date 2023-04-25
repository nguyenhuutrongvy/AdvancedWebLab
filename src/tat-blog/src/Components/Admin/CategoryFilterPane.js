import { useState, useEffect } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { Link } from 'react-router-dom';
import { getCategories } from '../../Services/BlogRepository';
import { useSelector, useDispatch } from 'react-redux';
import {
    reset,
    updateKeyword
} from '../../Redux/Reducer';

const CategoryFilterPane = () => {
    const categoryFilter = useSelector(state => state.categoryFilter),
    dispatch = useDispatch(),
    [filter, setFilter] = useState();

    const handleReset = (e) => {
        dispatch(reset());
    };

    // const current = new Date(),
    //     [keyword, setKeyword] = useState(''),
    //     [authorId, setAuthorId] = useState(''),
    //     [categoryId, setCategoryId] = useState(''),
    //     [year, setYear] = useState(current.getFullYear()),
    //     [month, setMonth] = useState(current.getMonth()),
    //     [categoryFilter, setPostFilter] = useState({
    //         authorList: [],
    //         categoryList: [],
    //         monthList: []
    //     });

    const handleSubmit = (e) => {
        e.preventDefault();
    };

    useEffect(() => {
        getCategories().then(data => {
            if (data) {
                setFilter({});
            } else {
                setFilter({});
            }
        })
    }, [])

    return (
        <Form method='get'
            onReset={handleReset}
            className='row gy-2 gx-3 align-items-center p-2'>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Keyword
                </Form.Label>
                <Form.Control
                    type='text'
                    placeholder='Nhập từ khóa...'
                    name='name'
                    value={categoryFilter.name}
                    onChange={e => dispatch(updateKeyword(e.target.value))} />
            </Form.Group>            
            <Form.Group className='col-auto'>
                <Button variant='danger' type='reset'>
                    Tìm/ Lọc
                </Button>
                <Link to='/admin/posts/edit' className='btn btn-success ms-2'>Thêm mới</Link>
            </Form.Group>
        </Form>
    );
}

export default CategoryFilterPane;
