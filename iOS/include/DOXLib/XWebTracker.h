//
//  XWebTracker.h
//  DOX
//
//  Created by Woncheol Heo on 2019. 6. 5..
//  Copyright © 2019년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <WebKit/WebKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface XWebTracker : NSObject

+ (XWebTracker *)sharedInstance;

- (void)setWebView:(UIWebView *)webView request:(NSURLRequest *)request;
- (void)setWkWebView:(WKWebView *)wkWebView reqeust:(NSURLRequest *)request;
@end

NS_ASSUME_NONNULL_END
