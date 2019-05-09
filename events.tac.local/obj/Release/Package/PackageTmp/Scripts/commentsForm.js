function createCommentItem(form, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });
    var obj = {
        ItemName: 'comment - ' + form.name.value,
        TemplateID: '{57565C84-D714-4C4D-9FAC-3D0FF43D35CF}',
        Name: form.name.value,
        Comment: form.comment.value
    };
    service.create(obj).path(path).execute()
    .then(function (item) {
        form.name.value = form.comment.value = '';
        window.alert('Thanks. Your message will show on the site shortly');
    })
    .fail(function (err) { window.alert(err); });
    event.preventDefault();
    return false;
}
