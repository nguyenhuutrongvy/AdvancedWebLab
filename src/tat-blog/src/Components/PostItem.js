import TagList from './TagList';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
// import { isEmptyOrSpaces } from '../Utils/Utils';

const PostList = ({ postItem }) => {
    var items = ["1", "2", "3", "4", "5"];
    var item = items[Math.floor(Math.random() * items.length)];
    let imageUrl = process.env.PUBLIC_URL + '/images/image_' + item + '.jpg';

    // let imageUrl = isEmptyOrSpaces(postItem.imageUrl) ? process.env.PUBLIC_URL + '/images/image_1.jpg' : `${postItem.imageUrl}`;

    let postedDate = new Date(postItem.postedDate);

    let year = postedDate.getFullYear();
    let month = postedDate.getMonth() + 1;
    let day = postedDate.getDate();

    return (
        <article className='blog-entry mb-4'>
            <Card>
                <div className='row g-0'>
                    <div className='col-md-4'>
                        <Card.Img variant='top' src={imageUrl} alt={postItem.title} />
                    </div>
                    <div className='col-md-8'>
                        <Card.Body>
                            <Card.Title>
                                <Link to={`/blog/post/${year}/${month}/${day}/${postItem.urlSlug}`} className='text-primary m-1 text-decoration-none'>
                                    {postItem.title}
                                </Link>
                            </Card.Title>
                            <Card.Text>
                                <small className='text-muted'>Tác giả:</small>
                                <Link to={`/blog/author/${postItem.author.urlSlug}`} className='text-primary m-1 text-decoration-none'>
                                    {postItem.author.fullName}
                                </Link>
                                <small className='text-muted'>Chủ đề:</small>
                                <Link to={`/blog/category/${postItem.category.urlSlug}`} className='text-primary m-1 text-decoration-none'>
                                    {postItem.category.name}
                                </Link>
                            </Card.Text>
                            <Card.Text>
                                {postItem.shortDescription}
                            </Card.Text>
                            <div className='tag-list'>
                                <TagList tagList={postItem.tags}></TagList>
                            </div>
                            <div className='text-end'>
                                <Link to={`/blog/post?year=${postedDate.getFullYear()}&month=${postedDate.getMonth()}&day=${postedDate.getDate()}&slug=${postItem.urlSlug}`}
                                    className='btn btn-primary'
                                    title={postItem.title}>
                                    Xem chi tiết
                                </Link>
                            </div>
                        </Card.Body>
                    </div>
                </div>
            </Card>
        </article>
    );
};

export default PostList;