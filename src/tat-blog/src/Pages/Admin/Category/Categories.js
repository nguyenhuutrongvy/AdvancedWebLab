import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams, Navigate } from 'react-router-dom';
import { getCategories } from '../../../Services/BlogRepository';
import Loading from '../../../Components/Loading';
import CategoryFilterPane from '../../../Components/Admin/PostFilterPane';
import { useSelector } from 'react-redux';

const Categories = () => {
    const [categoriesList, setCategoriesList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true),
        categoryFilter = useSelector(state => state.categoryFilter);
    let { id } = useParams();

    useEffect(() => {
        document.title = 'Danh sách chủ đề';
        getCategories(categoryFilter.keyword).then(data => {
            if (data)
                setCategoriesList(data.items);
            else
                setCategoriesList([]);
            setIsVisibleLoading(false);
        });
    }, [
        categoryFilter.keyword
    ]);

    return (
        <>
            <h1>Danh sách chủ đề</h1>
            <CategoryFilterPane />
            {isVisibleLoading ? <Loading /> :
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>Tên</th>
                            <th>Miêu tả</th>
                            <th>Hiển thị</th>
                        </tr>
                    </thead>
                    <tbody>
                        {categoriesList.length > 0 ? categoriesList.map((item, index) =>
                            <tr key={index}>
                                <td>
                                    <Link
                                        to={`/admin/categories/edit/${item.id}`}
                                        className='text-bold'>
                                        {item.name}
                                    </Link>
                                </td>
                                <td>{item.description}</td>
                                <td>{item.showOnMenu ? "Có" : "Không"}</td>
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className='text-danger text-center'>Không tìm thấy chủ đề nào</h4>
                                </td>
                            </tr>}
                    </tbody>
                </Table>
            }
        </>
    );
}

export default Categories;