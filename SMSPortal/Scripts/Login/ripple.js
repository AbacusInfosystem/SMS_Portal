// RIPPLE
  // CODE FROM HENDRY SADRAK'S PEN - http://codepen.io/hendrysadrak/pen/yNKZWO

	$(document).on('click', '.ripple', function(e) {

		var $ripple = $('<span class="rippling" />'),
			$button = $(this),
			btnOffset = $button.offset(),
			xPos = e.pageX - btnOffset.left,
			yPos = e.pageY - btnOffset.top,
			size = 0,
			animateSize = parseInt(Math.max($button.width(), $button.height()) * Math.PI);

		$ripple.css({
				top: yPos,
				left: xPos,
				width: size,
				height: size,
				backgroundColor: $button.attr("ripple-color"),
				opacity: $button.attr("ripple-opacity")
			})
			.appendTo($button)
			.animate({
				width: animateSize,
				height: animateSize,
				opacity: 0
			}, 500, function() {
				$(this).remove();
			});
	});